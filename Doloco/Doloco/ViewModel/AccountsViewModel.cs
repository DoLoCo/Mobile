using System;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
	public class AccountsViewModel:BaseViewModel
	{
		private readonly INavigation _navigation;
		public AccountsViewModel(INavigation navigation)
		{
			this._navigation = navigation;
		}
	}
}

