using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Doloco.Views;
using Xamarin.Forms;
using Doloco.ViewModel;
using Doloco.Models;

namespace Doloco.Pages
{
    public class SettingsPage:ContentPage
    {
        public class SettingsOptionItem
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

		public SettingsPage() {
			BindingContext = new SettingsViewModel(Navigation);
		    var layout = new StackLayout();

		    var paymentOptions = new List<SettingsOptionItem>
		    {
                new SettingsOptionItem
                {
                    Name = "Bank Accounts",
                    Description = "Create And View Connected Bank Accounts"
                }
		    };

            var cell = new DataTemplate(typeof(ListTextCell));
            cell.SetBinding(TextCell.TextProperty, "Name");
            cell.SetBinding(TextCell.DetailProperty, "Description");

            var optionsListView = new ListView { ItemsSource = paymentOptions, ItemTemplate = cell };
			optionsListView.ItemSelected += (sender, e) => Navigation.PushAsync(new AccountsPage());

			layout.Children.Add (optionsListView);

			Content = layout;
		}
    }
}
