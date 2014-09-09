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
    public class CircleViewModel:BaseViewModel
    {
        private readonly INavigation _navigation;
        public Organization OrganizationModel;
        public IEnumerable CampaignsModel;

        public CircleViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }
    }
}
