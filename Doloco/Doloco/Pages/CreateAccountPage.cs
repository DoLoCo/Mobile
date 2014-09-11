using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var button = new Button { Text = "Create Account", BackgroundColor = Helpers.Color.Blue.ToFormsColor() };
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
                        var token =
                            await
                                App.ApiClient.RegisterUserAsync(email.Text, firstName.Text, lastName.Text, password.Text,
                                    passwordConfirm.Text);
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
            var cancel = new Button { Text = "Cancel", BackgroundColor = Helpers.Color.Blue.ToFormsColor() };
            cancel.Clicked += (sender, e) => MessagingCenter.Send<ContentPage>(this, "Login");

            email = new Entry { Text = "", Placeholder = "Email"};
            firstName = new Entry { Text = "", Placeholder = "First Name"};
            lastName = new Entry { Text = "", Placeholder = "Last Name"};
            password = new Entry { Text = "", Placeholder = "Password", IsPassword = true};
            passwordConfirm = new Entry { Text = "", Placeholder = "Re-enter Password", IsPassword = true };
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(10, 40, 10, 10),
                    Children =
                    {
                        new Label {Text = "Create Account", Font = Font.SystemFontOfSize(NamedSize.Large)},
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
