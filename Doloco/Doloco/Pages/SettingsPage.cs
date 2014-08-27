using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Doloco.ViewModel;
using Doloco.Models;

namespace Doloco.Pages
{
    public class SettingsPage:ContentPage
    {
		public SettingsPage() {
			BindingContext = new SettingsViewModel(Navigation);
			var layout = new StackLayout();

			var header = new Label {
				Text = "Bank Settings"
			};

			layout.Children.Add (header);

			var paymentOptions = new string [] {
				"Receiving Accounts",
				"Payment Accounts"
			};

			var optionsListView = new ListView {
				ItemsSource = paymentOptions,
				RowHeight = 40
			};

			optionsListView.ItemSelected += (sender, e) => {
				if (e.SelectedItem.ToString() == "Receiving Accounts"){
					Navigation.PushAsync(new AccountsPage(AccountType.ReceivingAccount));
				} else {
					Navigation.PushAsync(new AccountsPage(AccountType.PaymentAccount));
				}
			};

			layout.Children.Add (optionsListView);

			Content = layout;
		}
    }
}
