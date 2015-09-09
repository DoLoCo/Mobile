using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Doloco.Pages;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
	public class AddCircleAccountViewModel:BaseViewModel
	{
		private readonly INavigation _navigation;
	    private readonly int _cirlceId;

		public AddCircleAccountViewModel(INavigation navigation, int circleId)
		{
			_navigation = navigation;
		    _cirlceId = circleId;
		}

		private Command _addCommand;
		public const string AddCommandPropertyName = "AddCommand";
		public Command AddCommand
		{
			get
			{
				return _addCommand ?? (_addCommand = new Command(async () => await ExecuteAddCommand()));
			}
		}

		protected async Task ExecuteAddCommand()
		{
		    await _navigation.PushAsync(new CirclePage(_cirlceId));
		}
	}
}

