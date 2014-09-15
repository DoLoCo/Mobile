using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
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
        private Map _map;
        private readonly Distance _radius = Distance.FromMiles(0.3);

        public HomePage()
        {
            var viewModel = new CampaignMapViewModel(App.UserLatitude, App.UserLongitude);
            BindingContext = viewModel;

            var stack = new StackLayout { Spacing = 0 };

            var map = MakeMap();

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
                var pins = ViewModel.LoadPins(position.Latitude, position.Longitude);
                _map = PinnedMap(pins);
                _map.MoveToRegion(MapSpan.FromCenterAndRadius(position,
                    Distance.FromMiles(0.1)));
            };
            stack.Children.Add(searchAddress);

            map.VerticalOptions = LayoutOptions.FillAndExpand;
            map.HeightRequest = 100;
            map.WidthRequest = 960;

            stack.Children.Add(map);
            Content = stack;
        }

        public Map MakeMap()
        {

            var pins = ViewModel.LoadPins(App.UserLatitude, App.UserLongitude);

            var map = PinnedMap(pins);

/*            map. += (sender, args)=>
            {
                Debug.WriteLine(args.SelectedItem);
/*                var pin = args.SelectedItem as Pin;
                var details = PinMap[pin];
/*                var page = new CampaignPage(details.);#2#
                Navigation.PushAsync(page);#1#
            };*/

            return map;
        }

        private Map PinnedMap(IList<Pin> pins)
        {
            if (_map == null)
                _map = new Map(MapSpan.FromCenterAndRadius(new Position(App.UserLatitude, App.UserLongitude), _radius));

            if (pins == null || !pins.Any())
            {
                return _map;
            }

            var dict = pins.Zip(ViewModel.Campaigns, (p, m) => new KeyValuePair<Pin, Campaign>(p, m)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            PinMap = dict;
            _map = new Map(MapSpan.FromCenterAndRadius(pins[0].Position, _radius));

            foreach (var p in pins)
            {
                _map.Pins.Add(p);
            }

            return _map;
        }
    }
}
