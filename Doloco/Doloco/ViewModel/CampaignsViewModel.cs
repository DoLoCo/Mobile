using System;
using System.Collections;
using System.Linq;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
    public class CampaignsViewModel
    {
        private readonly INavigation _navigation;
		public IEnumerable Model;

		public CampaignsViewModel(INavigation navigation)
		{
			this._navigation = navigation;
		}
    }
}

