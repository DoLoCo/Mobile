using System;
using System.Threading.Tasks;
using Doloco.Helpers;
using Doloco.Pages;
using Doloco.Views;
using DolocoApiClient.Models;
using Xamarin.Forms;
using Doloco.ViewModel;
using Doloco.Models;

namespace Doloco
{
	public class AccountsPage: ContentPage
	{
		public AccountsPage ()
		{

		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        LoadPage();
	    }

	    public async Task LoadPage()
	    {
            var viewModel = new AccountsViewModel(Navigation);
	        BindingContext = viewModel;

            var layout = new StackLayout();

            var addButton = new Button
            {
                Text = "Add Account"
            };
            addButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushModalAsync(new AddBankAccountPage());
            };

            layout.Children.Add(addButton);

	        try
	        {
	            viewModel.Model = await App.ApiClient.GetBankAccountsAsync();
	        }
	        catch (Exception ex)
	        {
                var page = new ContentPage();
                page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
	        }

            var cell = new DataTemplate(typeof(ListTextCell));
            cell.SetBinding(ImageCell.ImageSourceProperty, new Binding("Status") { Converter = new BankAccountStatusIconConverter() });
            cell.SetBinding(TextCell.TextProperty, "BankAccountName");
            cell.SetBinding(TextCell.DetailProperty, "LastFour");

            var list = new ListView { ItemsSource = viewModel.Model, ItemTemplate = cell };
	        list.ItemSelected += async (sender, e) =>
	        {
                var selectedAccount = (BankAccount)e.SelectedItem;
	            if (selectedAccount.Status == "Verified") return;

	            var verifyPage = new VerifyBankPage(selectedAccount.Id);
	            await Navigation.PushAsync(verifyPage);
	        };
            layout.Children.Add(list);

            Content = layout;
	    }
	}
}

