using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.Views;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class CreateAccountPage : ContentPage
    {
        readonly Entry email;
        readonly Entry firstName;
        readonly Entry lastName;
        readonly Entry password;
        readonly Entry passwordConfirm;

        public CreateAccountPage(ILoginManager ilm)
        {
            var button = new Button
            {
                Text = "Create Account",
                BackgroundColor = Color.FromHex("4f81bc"),
                BorderColor = Color.Transparent,
                TextColor = Color.White,
                BorderRadius = 20
            };
            button.Clicked += async (sender, e) =>
            {
                if (String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(password.Text) 
                    || String.IsNullOrEmpty(passwordConfirm.Text) || String.IsNullOrEmpty(firstName.Text) 
                    || String.IsNullOrEmpty(lastName.Text))
                {
                    await DisplayAlert("Validation Error", "You are missing some required fields", "Re-try");
                }
                else
                {
                    try
                    {
                        var payload =
                            await
                                App.ApiClient.RegisterUserAsync(email.Text, firstName.Text, lastName.Text, password.Text,
                                    passwordConfirm.Text);

                        object token;
                        object user;
                        payload.TryGetValue("Token", out token);
                        payload.TryGetValue("User", out user);

                        App.Token = (string)token;
                        App.CurrentUser = (User)user;

                        App.NearbyCampaigns = await App.ApiClient.GetNearbyCampaignsAsync(App.UserLatitude, App.UserLongitude);

                        ilm.ShowMainPage();
                    }
                    catch (Exception ex)
                    {
                        var page = new ContentPage();
                        var result = page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
                    }
                }

            };
            var cancel = new Button
            {
                Text = "Cancel",
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                BorderColor = Color.Transparent
            };
            cancel.Clicked += (sender, e) => MessagingCenter.Send<ContentPage>(this, "Login");

            email = new RoundedEntry
            {
                Text = "", 
                Placeholder = "Email",
                TextColor = Color.White,
                BackgroundColor = Color.Transparent
            };
            firstName = new RoundedEntry
            {
                Text = "",
                Placeholder = "First Name",
                TextColor = Color.White,
                BackgroundColor = Color.Transparent
            };
            lastName = new RoundedEntry
            {
                Text = "", 
                Placeholder = "Last Name",
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
            passwordConfirm = new RoundedEntry
            {
                Text = "", 
                Placeholder = "Re-enter Password", 
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
                        email,
                        firstName,
                        lastName,
                        password,
                        passwordConfirm,
                        button,
                        cancel
                    }
                }
            };
        }
    }
}
