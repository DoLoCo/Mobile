using System;
using System.Collections.Generic;
using System.Linq;
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
			_navigation = navigation;
		}
	}
}

