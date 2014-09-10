using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DolocoApiClient.Models;
using Doloco.ViewModel;
using Doloco.Models;

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
            Detail = new NavigationPage(displayPage)
            {
                BarBackgroundColor = Helpers.Color.Orange.ToFormsColor()
            };

            IsPresented = false;
        }

        Page PageForOption(OptionItem option)
        {
			if (option.Title == "Home")
				return new HomePage ();
			else if (option.Title == "Circles") {
				return new CirclesPage ();
			} else if (option.Title == "Campaigns") {
			}
			else if (option.Title == "Settings")
				return new SettingsPage ();

            throw new NotImplementedException("Unknown menu option: " + option.Title);
        }
    }
}
