using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class CirclePage:ContentPage
    {
        private readonly int _circleId;
        public CirclePage(int circleId)
        {
            _circleId = circleId;
        }

        protected override void OnAppearing()
        {
            LoadPage();
        }

        public async Task LoadPage()
        {
            var viewModel = new CircleViewModel(Navigation);
            BindingContext = viewModel;

            var stack = new StackLayout();

            try
            {
                viewModel.OrganizationModel = await App.ApiClient.GetOrganizationAsync(_circleId);
                viewModel.CampaignsModel = await App.ApiClient.GetOrganizationCampaignAsync(_circleId);
            } catch (Exception ex)
            {
                var page = new ContentPage();
                page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }

            var headerLabel = new Label
            {
                Text = viewModel.OrganizationModel.Name
            };
            stack.Children.Add(headerLabel);

            var descLabel = new Label
            {
                Text = viewModel.OrganizationModel.Description
            };
            stack.Children.Add(descLabel);

            var button = new Button
            {
                Text = "Add Campaign"
            };
            button.Clicked += async (sender, e) =>
            {
                var createCampaignPage = new CreateCampaignPage(_circleId);
                await Navigation.PushAsync(createCampaignPage);
            };
            stack.Children.Add(button);

            var cell = new DataTemplate(typeof(TextCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            cell.SetBinding(TextCell.DetailProperty, "Description");

            var list = new ListView { ItemsSource = viewModel.CampaignsModel, ItemTemplate = cell };
            list.ItemSelected += async (sender, e) =>
            {
                var selectedCampaign = (Campaign) e.SelectedItem;
                var campaignPage = new CampaignPage(selectedCampaign.Id, _circleId);

                await Navigation.PushAsync(campaignPage);
            };
            stack.Children.Add(list);

            Content = stack;
        }
    }
}
