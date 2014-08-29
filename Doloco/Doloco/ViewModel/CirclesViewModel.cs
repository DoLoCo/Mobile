using System;
using System.Threading.Tasks;
using System.Collections;
using Xamarin.Forms;
using DolocoApiClient.Models;

namespace Doloco.ViewModel
{
	public class CirclesViewModel
	{
		private readonly INavigation _navigation;
		public IEnumerable Model;

		public CirclesViewModel(INavigation navigation)
		{
			this._navigation = navigation;

			try
			{
				var organizations = App.ApiClient.GetMyOrganizationsAsync();

				this.Model = organizations.Result;
			}
			catch (Exception ex) {
				var page = new ContentPage();
				var result = page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
			}
		}
	}
}

