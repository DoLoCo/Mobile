using System;
using System.Collections.Generic;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
	public class AccountsViewModel:BaseViewModel
	{
		private readonly INavigation _navigation;
	    public IEnumerable<BankAccount> Model;
 
		public AccountsViewModel(INavigation navigation)
		{
			this._navigation = navigation;
		}
	}
}

