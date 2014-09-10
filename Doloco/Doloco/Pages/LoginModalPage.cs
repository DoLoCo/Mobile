using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class LoginModalPage : CarouselPage
    {
        private ContentPage login, create;

        public LoginModalPage(ILoginManager ilm)
        {
            login = new LoginPage(ilm);
            create = new CreateAccountPage(ilm);
            this.Children.Add(login);
            this.Children.Add(create);

            BackgroundColor = Helpers.Color.Orange.ToFormsColor();

            MessagingCenter.Subscribe<ContentPage>(this, "Login", (sender) => {
                                                                                  this.SelectedItem = login;
            });
            MessagingCenter.Subscribe<ContentPage>(this, "Create", (sender) => {
                                                                                   this.SelectedItem = create;
            });
        }
    }
}
