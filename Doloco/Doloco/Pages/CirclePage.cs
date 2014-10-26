using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Doloco.Views;
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

            this.Title = viewModel.OrganizationModel.Name;

            var headerLabel = new Label
            {
                Text = viewModel.OrganizationModel.Name,
                TextColor = Color.White,
                Font = Font.BoldSystemFontOfSize(20),
                HorizontalOptions = LayoutOptions.Center
            };

            var descLabel = new Label
            {
                Text = viewModel.OrganizationModel.Description,
                TextColor = Color.White
            };


            var cirleDetailView = new StackLayout
            {
                BackgroundColor = Helpers.Color.DarkGray.ToFormsColor(),
                Padding = new Thickness(10, 10, 10, 20),
                Children =
                {
                    headerLabel,
                    descLabel
                }
            };

            stack.Children.Add(cirleDetailView);

            var cell = new DataTemplate(typeof(ListTextCell));
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

            var button = new DefaultButton
            {
                Text = "Add Campaign"
            };
            button.Clicked += async (sender, e) =>
            {
                var createCampaignPage = new CreateCampaignPage(_circleId);
                await Navigation.PushAsync(createCampaignPage);
            };

            stack.Children.Add(button);

            Content = stack;
        }
    }
}
