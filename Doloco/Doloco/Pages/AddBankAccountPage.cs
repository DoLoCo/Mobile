using System;
using Xamarin.Forms;
using Doloco.Models;
using Doloco.ViewModel;

namespace Doloco
{
	public class AddBankAccountPage:ContentPage
	{
		public AddBankAccountPage ()
		{
			BindingContext = new AddAccountViewModel(Navigation);
			var layout = new StackLayout();

		    var accountType = new Picker() {Title = "Account Type"};
            accountType.Items.Add("Checking");
            accountType.Items.Add("Savings");
			accountType.SetBinding(Picker.SelectedIndexProperty, AddAccountViewModel.AccountTypePropertyName);
			layout.Children.Add(accountType);

			var accountName = new Entry { Placeholder = "Account Name" };
			accountName.SetBinding(Entry.TextProperty, AddAccountViewModel.AccountNamePropertyName);
			layout.Children.Add(accountName);

			var accountNumber = new Entry { Placeholder = "Account Number" };
			accountNumber.SetBinding(Entry.TextProperty, AddAccountViewModel.AccountNumberPropertyName);
			layout.Children.Add(accountNumber);

			var routingNumber = new Entry { Placeholder = "Routing Number" };
			routingNumber.SetBinding(Entry.TextProperty, AddAccountViewModel.RoutingNumberPropertyName);
			layout.Children.Add(routingNumber);

			var button = new Button { Text = "Add Account", TextColor = Color.White };
			button.SetBinding(Button.CommandProperty, AddAccountViewModel.AddCommandPropertyName);
			layout.Children.Add (button);

			Content = layout;
		}
	}
}