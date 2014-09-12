using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using DolocoApiClient.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Doloco.Pages
{
    public class CampaignMapPage:ContentPage
    {

        public CampaignMapPage()
        {
            var latLabel = new Label
            {
                Text = App.UserLatitude.ToString()
            };

            var lngLabel = new Label()
            {
                Text = App.UserLongitude.ToString()
            };

            var layout = new StackLayout
            {
                Children =
                {
                    latLabel,
                    lngLabel
                }
            };

            Content = layout;
        }
    }
}
