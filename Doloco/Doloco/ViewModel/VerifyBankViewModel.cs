using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
    public class VerifyBankViewModel:BaseViewModel
    {
        private INavigation _navigation;
        private int _bankAccountId;
        public VerifyBankViewModel(INavigation navigation, int accountId)
        {
            _navigation = navigation;
            _bankAccountId = accountId;
        }


        public const string AmountOnePropertyName = "AmountOne";
        private decimal _amountOne;
        public decimal AmountOne
        {
            get { return _amountOne; }
            set { SetProperty(ref _amountOne, value, AmountOnePropertyName); }
        }

        public const string AmountTwoPropertyName = "AmountTwo";
        private decimal _amountTwo;
        public decimal AmountTwo
        {
            get { return _amountTwo; }
            set { SetProperty(ref _amountTwo, value, AmountTwoPropertyName); }
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
            try
            {
                await App.ApiClient.VerifyBankAccountAsync(_bankAccountId, _amountOne, _amountTwo);
                await _navigation.PopAsync();
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }
            
        }

    }
}
