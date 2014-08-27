using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Doloco.ViewModel;

namespace Doloco
{
	public class AddAccountViewModel:BaseViewModel
	{
		private readonly INavigation _navigation;
		public AddAccountViewModel(INavigation navigation)
		{
			this._navigation = navigation;
		}

		public const string AccountTypePropertyName = "AccountType";
		private string accountType = string.Empty;
		public string AccountType
		{
			get { return accountType; }
			set { SetProperty(ref accountType, value, AccountTypePropertyName); }
		}

		public const string AccountPropertyName = "AccountName";
		private string accountName = string.Empty;
		public string AccountName
		{
			get { return accountName; }
			set { SetProperty(ref accountName, value, AccountPropertyName); }
		}

		public const string AccountNumberPropertyName = "AccountNumber";
		private string accountNumber = string.Empty;
		public string AccountNumber
		{
			get { return accountNumber; }
			set { SetProperty(ref accountNumber, value, AccountNumberPropertyName); }
		}

		public const string RoutingNumberPropertyName = "RoutingNumber";
		private string routingNumber = string.Empty;
		public string RoutingNumber
		{
			get { return routingNumber; }
			set { SetProperty(ref routingNumber, value, RoutingNumberPropertyName); }
		}

		private Command addCommand;
		public const string AddCommandPropertyName = "AddCommand";
		public Command AddCommand
		{
			get
			{
				return addCommand ?? (addCommand = new Command(async () => await ExecuteAddCommand()));
			}
		}

		protected async Task ExecuteAddCommand()
		{
			Debug.WriteLine(accountName);
		}
	}
}

