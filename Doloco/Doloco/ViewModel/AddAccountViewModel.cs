using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
	public class AddAccountViewModel:BaseViewModel
	{
		private readonly INavigation _navigation;

		public AddAccountViewModel(INavigation navigation)
		{
			_navigation = navigation;
		}
	}
}

