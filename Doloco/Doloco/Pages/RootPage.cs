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

        public RootPage()
        {
            var optionsPage = new MenuPage { Icon = "Icon.png", Title = "menu" };

            optionsPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as OptionItem);

            Master = optionsPage;

            NavigateTo(optionsPage.Menu.ItemsSource.Cast<OptionItem>().First());
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
            if (option.Title == "Home")
                return new MasterPage<HomePage>(option);

            throw new NotImplementedException("Unknown menu option: " + option.Title);
        }
    }
}
