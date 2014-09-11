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
        OptionItem _previousItem;

        public RootPage()
        {
            var optionsPage = new MenuPage { Icon = "apptheme_ic_navigation_drawer.png", Title = "menu" };

            optionsPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as OptionItem);

            Master = optionsPage;

            NavigateTo(optionsPage.Menu.ItemsSource.Cast<OptionItem>().First());
        }

        void NavigateTo(OptionItem option)
        {
            if (_previousItem != null)
                _previousItem.Selected = false;

            option.Selected = true;
            _previousItem = option;

            var displayPage = PageForOption(option);
            displayPage.SetValue(TitleProperty, option.Title);
            Detail = new NavigationPage(displayPage);

            Detail.SetValue(IconProperty, option.Icon);

            IsPresented = false;
        }

        static Page PageForOption(OptionItem option)
        {
            switch (option.Title)
            {
                case "Home":
                    return new HomePage();
                case "Circles":
                    return new CirclesPage();
                case "Settings":
                    return new SettingsPage ();
                default:
                    throw new NotImplementedException("Unknown menu option: " + option.Title);
            }
        }
    }
}
