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

		public CampaignsViewModel(INavigation navigation, int? orgId = null)
		{
			this._navigation = navigation;
		    var campaigns = new Campaign[]{};

		    try
			{
			    if (orgId != null)
			    {
			        /* get specific circle campaigns */
			    }
			    else
			    {
                    this.Model = campaigns;
			    }
			}
			catch (Exception ex) {
				var page = new ContentPage();
				page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
			}
		}
    }
}

