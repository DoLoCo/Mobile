using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Doloco.ViewModel
{
    public class CampaignMapViewModel:BaseViewModel
    {
        private readonly ContentPage _errorPage = new ContentPage();
        public IEnumerable<Campaign> Campaigns;
        public CampaignMapViewModel(double lat, double lng)
        {
            Title = "Map";
            Icon = "Icon.png";
        }

        public List<Pin> LoadPins(double latitude, double longitude)
        {
            var pins = new List<Pin>();

            ExecuteLoadModelsCommand(latitude, longitude);

            if (Campaigns == null || !Campaigns.Any()) return pins;

            pins = Campaigns.Select(model =>
            {
                var campaign = (Campaign) model;
                var organization = campaign.Organization;
                if (organization.Lat != null && organization.Lng != null)
                {
                    var position = new Position((double) organization.Lat, (double) organization.Lng);
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = campaign.ToString(),
                        Address = organization.AddressLine1.ToString()
                    };
                    return pin;
                }

                return null;
            }).ToList();

            return pins;
        }

        private async void ExecuteLoadModelsCommand(double lat, double lng)
        {
            try
            {
                var campaignList = await App.ApiClient.GetNearbyCampaignsAsync(lat, lng);
                Campaigns = campaignList;
            }
            catch (Exception ex)
            {
                _errorPage.DisplayAlert("Error", ex.Message, "Ok", "Cancel");
            }
        }
    }
}
