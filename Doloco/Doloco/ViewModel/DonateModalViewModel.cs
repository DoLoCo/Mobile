using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
    public class DonateModalViewModel:BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly int _organizationId;
        private readonly int _campaignId;
        private readonly Dictionary<string, BankAccount> _bankAccountDictionary;
 
        public DonateModalViewModel(INavigation navigation, int organizationId, int campaignId, Dictionary<string, BankAccount> bankAccounts )
        {
            _navigation = navigation;
            _organizationId = organizationId;
            _campaignId = campaignId;
            _bankAccountDictionary = bankAccounts;
        }

        public const string AccountIdPropertyName = "AccountId";
        private string _accountId = string.Empty;
        public string AccountId
        {
            get { return _accountId; }
            set { SetProperty(ref _accountId, value, AccountIdPropertyName); }
        }

        public const string AmountPropertyName = "Amount";
        private string _amount = string.Empty;
        public string Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value, AmountPropertyName); }
        }

        private Command _donateCommand;
        public const string DonateCommandPropertyName = "DonateCommand";
        public Command DonateCommand
        {
            get
            {
                return _donateCommand ?? (_donateCommand = new Command(async () => await ExecuteDonateCommand()));
            }
        }

        protected async Task ExecuteDonateCommand()
        {
            BankAccount selectedAccount;
            _bankAccountDictionary.TryGetValue(_accountId, out selectedAccount);
        
            try
            {
                await
                    App.ApiClient.CreateOrganizationCampaignDonationAsync(_organizationId, _campaignId,
                        _amount, selectedAccount.Id);

                await _navigation.PopAsync();
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }
        }
    }
}
