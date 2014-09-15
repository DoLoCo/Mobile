using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class LoginPage : ContentPage
    {
        readonly Entry email;
        readonly Entry password;

        public LoginPage(ILoginManager ilm)
        {
            var button = new Button { Text = "Login" };
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

                        var token = await App.ApiClient.CreateSessionAsync(email.Text, password.Text);

                        App.Token = token;

                        ilm.ShowMainPage();
                    }
                    catch (Exception ex)
                    {
                        var page = new ContentPage();
                        var result = page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
                    }
                }
            };
            var create = new Button { Text = "Create Account" };
            create.Clicked += (sender, e) => MessagingCenter.Send<ContentPage>(this, "Create");

            var logoImage = new Image
            {
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Source = ImageSource.FromFile("splash.png")
            };

            email = new Entry { Text = "", Placeholder = "Email"};
            password = new Entry { Text = "", Placeholder = "Password", IsPassword = true};
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
                    BackgroundColor = Helpers.Color.FromHex(0xfa6a00).ToFormsColor()
                }
            };
        }
    }
}
