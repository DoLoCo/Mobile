using System;
using Xamarin.Forms;
using Doloco.ViewModel;
using Doloco.Models;

namespace Doloco
{
	public class AccountsPage: ContentPage
	{
		public AccountsPage (AccountType accountType)
		{
			BindingContext = new AccountsViewModel(Navigation);
			var layout = new StackLayout();

			var lblText = accountType.Equals (AccountType.PaymentAccount) ? "Payment Accounts" : "Receiving Accounts";

			var label = new Label {
				Text = lblText
			};

			layout.Children.Add(label);

			var addButton = new Button {
				Text = "Add Account"
			};
			addButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync(new AddBankAccountPage(accountType));
			};

			layout.Children.Add (addButton);

			Content = layout;
		}
	}
}

