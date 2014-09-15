using System;
using Xamarin.Forms;
using Doloco.Models;
using Doloco.ViewModel;

namespace Doloco
{
	public class CreateCircleBankAccountPage:ContentPage
	{
        public CreateCircleBankAccountPage(int circleId)
		{
			BindingContext = new AddCircleAccountViewModel(Navigation, circleId);
			var layout = new StackLayout();

            var label = new Label
            {
                Text = "Circle Bank Accoount"
            };
            layout.Children.Add(label);

		    var accountType = new Picker() {Title = "Account Type"};
            accountType.Items.Add("Checking");
            accountType.Items.Add("Savings");
            accountType.SetBinding(Picker.SelectedIndexProperty, AddCircleAccountViewModel.AccountTypePropertyName);
			layout.Children.Add(accountType);

			var accountName = new Entry { Placeholder = "Account Name" };
            accountName.SetBinding(Entry.TextProperty, AddCircleAccountViewModel.AccountNamePropertyName);
			layout.Children.Add(accountName);

			var accountNumber = new Entry { Placeholder = "Account Number" };
            accountNumber.SetBinding(Entry.TextProperty, AddCircleAccountViewModel.AccountNumberPropertyName);
			layout.Children.Add(accountNumber);

			var routingNumber = new Entry { Placeholder = "Routing Number" };
            routingNumber.SetBinding(Entry.TextProperty, AddCircleAccountViewModel.RoutingNumberPropertyName);
			layout.Children.Add(routingNumber);

            var button = new Button { Text = "Create Circle" };
            button.SetBinding(Button.CommandProperty, AddCircleAccountViewModel.AddCommandPropertyName);
			layout.Children.Add (button);

            Content = new ScrollView
            {
                Content = layout,
                Padding = new Thickness(10, 10, 10, 20)
            };
		}
	}
}