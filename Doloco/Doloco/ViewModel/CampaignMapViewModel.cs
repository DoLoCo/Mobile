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

        public IEnumerable<Campaign> LoadPins(double latitude, double longitude)
        {
            ExecuteLoadModelsCommand(latitude, longitude);

            return Campaigns;
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
