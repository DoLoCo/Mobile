using System;
using Xamarin.Forms;

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

