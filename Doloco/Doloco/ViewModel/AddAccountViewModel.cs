using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Doloco.ViewModel;

namespace Doloco
{
	public class AddAccountViewModel:BaseViewModel
	{
		private readonly INavigation _navigation;
	    private IDictionary<string, string> _accountTypes;

		public AddAccountViewModel(INavigation navigation)
		{
			_navigation = navigation;
            _accountTypes = new Dictionary<string, string>()
            {
                {"0", "Checking"},
                {"1", "savings"}
            };
		}

		public const string AccountTypePropertyName = "AccountType";
		private string _accountType = string.Empty;
		public string AccountType
		{
			get { return _accountType; }
			set { SetProperty(ref _accountType, value, AccountTypePropertyName); }
		}

		public const string AccountNamePropertyName = "AccountName";
		private string _accountName = string.Empty;
		public string AccountName
		{
			get { return _accountName; }
			set { SetProperty(ref _accountName, value, AccountNamePropertyName); }
		}

		public const string AccountNumberPropertyName = "AccountNumber";
		private string _accountNumber = string.Empty;
		public string AccountNumber
		{
			get { return _accountNumber; }
			set { SetProperty(ref _accountNumber, value, AccountNumberPropertyName); }
		}

		public const string RoutingNumberPropertyName = "RoutingNumber";
		private string _routingNumber = string.Empty;
		public string RoutingNumber
		{
			get { return _routingNumber; }
			set { SetProperty(ref _routingNumber, value, RoutingNumberPropertyName); }
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
		    string accountType; 
            _accountTypes.TryGetValue(_accountType, out accountType);

		    try
		    {
		        await App.ApiClient.CreateBankAccountAsync(_accountNumber, accountType, _routingNumber, _accountName);
		    }
		    catch (Exception ex)
		    {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
		    }
		}
	}
}

