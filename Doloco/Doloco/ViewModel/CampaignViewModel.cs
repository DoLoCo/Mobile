using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
    public class CampaignViewModel
    {
        private readonly INavigation _navigation;
        public IEnumerable DonationModel;
        public Campaign Model;

        public CampaignViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }
    }
}
