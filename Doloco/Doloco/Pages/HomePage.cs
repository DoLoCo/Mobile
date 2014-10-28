using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Doloco.Views;
using DolocoApiClient.Models;
using Doloco.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Doloco.Pages
{
    public class HomePage : ContentPage
    {

        private CampaignMapViewModel ViewModel
        {
            get { return BindingContext as CampaignMapViewModel; }
        }

        IDictionary<Pin, Campaign> PinMap;
        private readonly ListView _campaignList;
        private readonly Distance _radius = Distance.FromMiles(0.3);

        public HomePage()
        {
            var viewModel = new CampaignMapViewModel(App.UserLatitude, App.UserLongitude);
            BindingContext = viewModel;

            var stack = new StackLayout { Spacing = 0 };

            this.Title = "Nearby Campaigns";

            var searchAddress = new SearchBar { Placeholder = "Search Address", BackgroundColor = Color.White };
            searchAddress.SearchButtonPressed += async (e, a) =>
            {
                var addressQuery = searchAddress.Text;
                searchAddress.Text = "";
                searchAddress.Unfocus();

                var positions = (await (new Geocoder()).GetPositionsForAddressAsync(addressQuery)).ToList();
                if (!positions.Any())
                    return;

                var position = positions.First();
                var campaigns = ViewModel.LoadPins(position.Latitude, position.Longitude);
                _campaignList.ItemsSource = campaigns;
            };
            stack.Children.Add(searchAddress);

            var campaignItem = new DataTemplate(typeof (StripedViewCell));
            campaignItem.SetBinding(TextCell.TextProperty, "Name");
            campaignItem.SetBinding(TextCell.DetailProperty, "Description");
            var campaignListModel = viewModel.LoadPins(App.UserLatitude, App.UserLongitude);
            _campaignList = new StripedListView
            {
                ItemsSource = campaignListModel,
                ItemTemplate = campaignItem
            };
            _campaignList.ItemSelected += async (sender, e) =>
            {
                var selectedCampaign = (Campaign) e.SelectedItem;
                var campaignPage = new CampaignPage(selectedCampaign.Id, selectedCampaign.OrganizationId);

                await Navigation.PushAsync(campaignPage);
            };

            stack.Children.Add(_campaignList);
            Content = stack;
        }
    }
}
