using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.Models;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class RootPage : MasterDetailPage
    {
        OptionItem previousItem;
        private OptionItem loginOption;

        public RootPage()
        {
            var apiClient = new DolocoApiClient.DolocoApiClient("http://dolocony.asuscomm.com:3000/api/v1");
            loginOption = new LoginOptionItem();
            Master = PageForOption(loginOption);
            Detail = PageForOption(loginOption);
        }

        void NavigateTo(OptionItem option)
        {
            if (previousItem != null)
                previousItem.Selected = false;

            option.Selected = true;
            previousItem = option;

            var displayPage = PageForOption(option);

            Detail = new NavigationPage(displayPage);


            IsPresented = false;
        }

        Page PageForOption(OptionItem option)
        {
            if (option.Title == "Login")
                return new MasterPage<LoginPage>(option);

            throw new NotImplementedException("Unknown menu option: " + option.Title);
        }
    }
}
