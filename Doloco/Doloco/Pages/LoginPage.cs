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
        public LoginPage()
        {
            BindingContext = new LoginViewModel(Navigation);

            BackgroundColor = Helpers.Color.Blue.ToFormsColor();

            var layout = new StackLayout();

            var label = new Label
            {
                Text = "Doloco",
                Font = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                XAlign = TextAlignment.Center, // Center the text in the blue box.
                YAlign = TextAlignment.Center, // Center the text in the blue box.
            };

            layout.Children.Add(label);

            var username = new Entry { Placeholder = "Username" };
            username.SetBinding(Entry.TextProperty, LoginViewModel.UsernamePropertyName);
            layout.Children.Add(username);

            var password = new Entry { Placeholder = "Password", IsPassword = true};
            password.SetBinding(Entry.TextProperty, LoginViewModel.PasswordPropertyName);
            layout.Children.Add(password);



            var signIn = new Button { Text = "Sign In", TextColor = Color.White };
            signIn.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);

            var register = new Button {Text = "Register", TextColor = Color.White};
            register.SetBinding(Button.CommandProperty, LoginViewModel.RegisterCommandPropertyName);

            layout.Children.Add(signIn);
            layout.Children.Add(register);

            Content = layout;
        }
    }
}
