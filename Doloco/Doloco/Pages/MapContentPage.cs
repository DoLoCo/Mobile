using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class MapContentPage: ContentPage
    {
        public double InitLat;
        public double InitLng;
        public IEnumerable<Campaign> NearbyCampaigns;

        public MapContentPage()
        {
            InitLat = App.UserLatitude;
            InitLng = App.UserLongitude;
            NearbyCampaigns = App.NearbyCampaigns;
        }
    }
}
