using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Xamarin.Forms;

namespace Doloco.Pages
{
    class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            BindingContext = new RegisterViewModel(Navigation);

            var layout = new StackLayout();
            var isLoading = new ActivityIndicator();
            isLoading.SetBinding(ActivityIndicator.IsRunningProperty, RegisterViewModel.IsLoadingPropertyName);
            isLoading.SetBinding(IsVisibleProperty, RegisterViewModel.IsLoadingPropertyName);
            layout.Children.Add(isLoading);

            var email = new Entry { Placeholder = "Email" };
            email.SetBinding(Entry.TextProperty, RegisterViewModel.EmailPropertyName);
            layout.Children.Add(email);

            var firstName = new Entry { Placeholder = "First Name" };
            firstName.SetBinding(Entry.TextProperty, RegisterViewModel.FirstNamePropertyName);
            layout.Children.Add(firstName);

            var lastName = new Entry { Placeholder = "Last Name" };
            lastName.SetBinding(Entry.TextProperty, RegisterViewModel.LastNamePropertyName);
            layout.Children.Add(lastName);

            var password = new Entry { Placeholder = "Password", IsPassword = true };
            password.SetBinding(Entry.TextProperty, RegisterViewModel.PasswordPropertyName);
            layout.Children.Add(password);

            var passwordConfirm = new Entry { Placeholder = "Confirm Password", IsPassword = true };
            passwordConfirm.SetBinding(Entry.TextProperty, RegisterViewModel.PasswordConfirmPropertyName);
            layout.Children.Add(passwordConfirm);

            var register = new Button { Text = "Register", TextColor = Color.White };
            register.SetBinding(Button.CommandProperty, RegisterViewModel.RegisterCommandPropertyName);

            layout.Children.Add(register);

            Content = layout;
        }
    }
}
