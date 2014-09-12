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
                viewModel.DonationModel =
                    await App.ApiClient.GetOrganizationCampaignDonationsAsync(_organizationId, _campaignId);
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }

            this.Title = viewModel.Model.Title;

            var campaignView = _createCampaignLayout(viewModel);
            stack.Children.Add(campaignView);

            var cell = new DataTemplate(typeof(ListTextCell));
            cell.SetBinding(TextCell.TextProperty, "User.FirstName");
            cell.SetBinding(TextCell.DetailProperty, "ActualAmount");

            var list = new ListView { ItemsSource = viewModel.DonationModel, ItemTemplate = cell };
            stack.Children.Add(list);

            Content = stack;
        }

        private StackLayout _createCampaignLayout(CampaignViewModel viewModel)
        {
            var campaignImage = new Image
            {
                Source = new UriImageSource
                {
                    Uri = new Uri("http://s3.amazonaws.com/doloco_images/campaign_pics/placeholder-campaign.png"),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(5, 0, 0, 0)
                },
                Aspect = Aspect.Fill
            };

            var campaignDesc = new Label
            {
                Text = viewModel.Model.Description,
                TextColor = Color.White
            };

            var campaignProgressBar = new ProgressBar();

            if (viewModel.Model.TargetAmount != null)
            {
                var campaignProgress = (double) (viewModel.Model.DonationAmount/viewModel.Model.TargetAmount);
                campaignProgressBar.SetValue(ProgressBar.ProgressProperty, campaignProgress);
            }

            var donateButton = new Button
            {
                Text = "Donate Now!"
            };
            donateButton.Clicked += async (sender, e) =>
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

            var campaignView = new StackLayout
            {
                BackgroundColor = Helpers.Color.DarkGray.ToFormsColor(),
                Children =
                {
                    campaignImage,
                    campaignDesc,
                    campaignProgressBar,
                    donateButton
                }
            };

            return campaignView;
        }
    }
}
