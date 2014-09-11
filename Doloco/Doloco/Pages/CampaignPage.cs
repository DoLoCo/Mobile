using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Doloco.Views;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class CampaignPage:ContentPage
    {
        private readonly int _campaignId;
        private readonly int _organizationId;

        public CampaignPage(int campaignId, int organizationId)
        {
            _campaignId = campaignId;
            _organizationId = organizationId;
        }

        protected override void OnAppearing()
        {
            LoadPage();
        }

        public async Task LoadPage()
        {
            var viewModel = new CampaignViewModel(Navigation);
            BindingContext = viewModel;

            var stack = new StackLayout();

            try
            {
                viewModel.Model = await App.ApiClient.GetCampaignAsync(_campaignId);
                viewModel.DonationModel = await App.ApiClient.GetOrganizationCampaignDonationsAsync(_organizationId, _campaignId);
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }

            var headerLabel = new Label
            {
                Text = viewModel.Model.Title
            };
            stack.Children.Add(headerLabel);

            var descLabel = new Label
            {
                Text = viewModel.Model.Description
            };
            stack.Children.Add(descLabel);

            var button = new Button
            {
                Text = "Donate Now!"
            };
            button.Clicked += async (sender, e) =>
            {
                try
                {
                    var bankAccounts = await App.ApiClient.GetBankAccountsAsync();
                    var enumerable = bankAccounts as IList<BankAccount> ?? bankAccounts.ToList();
                    if (!enumerable.Any())
                    {
                        var page = new ContentPage();
                        page.DisplayAlert("Error", "Must Link a Bank Account First", "OK", "Cancel");
                    }
                    else
                    {
                        var donateModal = new DonatePage(_organizationId, _campaignId, enumerable);
                        await Navigation.PushAsync(donateModal);
                    }
                }
                catch (Exception ex)
                {
                    var page = new ContentPage();
                    page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
                }
            };
            stack.Children.Add(button);

            var cell = new DataTemplate(typeof(ListTextCell));
            cell.SetBinding(TextCell.TextProperty, "Amount");

            var list = new ListView { ItemsSource = viewModel.DonationModel, ItemTemplate = cell };
            stack.Children.Add(list);

            Content = stack;
        }
    }
}
