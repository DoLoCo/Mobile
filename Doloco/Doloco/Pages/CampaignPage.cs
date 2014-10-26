using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Doloco.Helpers;
using Doloco.ViewModel;
using Doloco.Views;
using DolocoApiClient.Models;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

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
                viewModel.CamapignUser = viewModel.Model.Organization.OrganizationAdmins.First().User;
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }

            this.Title = viewModel.Model.Title;

            var campaignView = await _createCampaignLayout(viewModel);
            stack.Children.Add(campaignView);

            var cell = new DataTemplate(typeof(UserActionCell));
            cell.SetBinding(TextCell.TextProperty, new Binding("User.FirstName") {StringFormat = "{0} Donated:"});
            cell.SetBinding(TextCell.DetailProperty, new Binding("Amount") {Converter = new CurrencyDisplayConverter()});
            cell.SetBinding(ImageCell.ImageSourceProperty, new Binding("User.Avatar") {Converter = new UserAvatarConverter()});

            var list = new ListView { ItemsSource = viewModel.DonationModel, ItemTemplate = cell };
            stack.Children.Add(list);

            var donateButton = new DefaultButton
            {
                Text = "Donate"
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
            stack.Children.Add(donateButton);

            Content = stack;
        }


        private async Task<StackLayout> _createCampaignLayout(CampaignViewModel viewModel)
        {
            
            var fullName = string.Format("{0} {1}", viewModel.CamapignUser.FirstName, viewModel.CamapignUser.LastName);
            var createdStr = String.Format("Created {0}", viewModel.Model.CreatedAt.ToString("D"));

            var campaignCreatorCell = new DataTemplate(typeof(ImageCell));
            campaignCreatorCell.SetValue(TextCell.TextProperty, fullName);
            campaignCreatorCell.SetValue(TextCell.DetailProperty, createdStr);
            var campaignCreatorList = new List<CampaignViewModel>
            {
                viewModel
            };

            var campaignCreatorView = new ListView { ItemsSource = campaignCreatorList, ItemTemplate = campaignCreatorCell, BackgroundColor = Color.White, RowHeight = 60, MinimumHeightRequest = 60};

            var campaignDesc = new Label
            {
                Text = viewModel.Model.Description,
                TextColor = Color.White,
                Font = Font.SystemFontOfSize(NamedSize.Large),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var campaignProgressBar = new ProgressBar();

            if (viewModel.Model.TargetAmount != null)
            {
                var campaignProgress = (double) (viewModel.Model.DonationAmount/viewModel.Model.TargetAmount);
                campaignProgressBar.SetValue(ProgressBar.ProgressProperty, campaignProgress);
            }

            

            var campaignView = new StackLayout
            {
                BackgroundColor = Helpers.Color.DarkGray.ToFormsColor(),
                Padding = new Thickness(10, 10, 10, 20),
                Children =
                {
                    campaignCreatorView,
                    campaignDesc,
                    campaignProgressBar
                }
            };

            return campaignView;
        }
    }
}
