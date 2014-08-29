using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using DolocoApiClient.Models;

namespace Doloco.ViewModel
{
	public class SettingsViewModel : BaseViewModel
	{
		private readonly INavigation _navigation;
		public SettingsViewModel(INavigation navigation)
		{
			this._navigation = navigation;
		}


	}
}

