using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Doloco.Views;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class LoginPage : ContentPage
    {
        readonly Entry email;
        readonly Entry password;

        public LoginPage(ILoginManager ilm)
        {
            var button = new Button
            {
                Text = "Login",
                BackgroundColor = Color.FromHex("4f81bc"),
                BorderColor = Color.Transparent,
                TextColor = Color.White,
                BorderRadius = 20
            };
            button.Clicked += async (sender, e) =>
            {
                if (String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(password.Text))
                {
                    await DisplayAlert("Validation Error", "Username and Password are required", "Re-try");
                }
                else
                {
                    try
                    {

                        var payload = await App.ApiClient.CreateSessionAsync(email.Text, password.Text);
                        App.NearbyCampaigns = await App.ApiClient.GetNearbyCampaignsAsync(App.UserLatitude, App.UserLongitude);

                        object token;
                        object user;
                        payload.TryGetValue("Token", out token);
                        payload.TryGetValue("User", out user);

                        App.Token = (string) token;
                        App.CurrentUser = (User) user;

                        ilm.ShowMainPage();
                    }
                    catch (Exception ex)
                    {
                        var page = new ContentPage();
                        var result = page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
                    }
                }
            };
            var create = new Button
            {
                Text = "Register",
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                BorderColor = Color.Transparent
            };
            create.Clicked += (sender, e) => MessagingCenter.Send<ContentPage>(this, "Create");

            var logoImage = new Image
            {
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = ImageSource.FromFile("splash.png")
            };

            email = new RoundedEntry
            {
                Text = "", 
                Placeholder = "Email Address",
                TextColor = Color.White,
                BackgroundColor = Color.Transparent
            };
            password = new RoundedEntry
            {
                Text = "", 
                Placeholder = "Password", 
                IsPassword = true,
                TextColor = Color.White,
                BackgroundColor = Color.Transparent
            };
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(10, 40, 10, 10),
                    Children =
                    {
                        logoImage,
                        email,
                        password,
                        button,
                        create
                    },
                    BackgroundColor = Helpers.Color.FromHex(0xf79748).ToFormsColor()
                }
            };
        }
    }
}
