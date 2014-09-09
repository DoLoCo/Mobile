using System;
using Xamarin.Forms;
using Doloco.ViewModel;
using Doloco.Models;

namespace Doloco
{
	public class AccountsPage: ContentPage
	{
		public AccountsPage ()
		{
			BindingContext = new AccountsViewModel(Navigation);
			var layout = new StackLayout();

			var addButton = new Button {
				Text = "Add Account"
			};
			addButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync(new AddBankAccountPage());
			};

			layout.Children.Add (addButton);

			Content = layout;
		}
	}
}

