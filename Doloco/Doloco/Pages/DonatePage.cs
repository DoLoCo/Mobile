using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class DonatePage:ContentPage
    {
        private readonly int _orgId;
        private readonly int _campaignId;
        private readonly IEnumerable<BankAccount> _bankAccounts;
        private readonly Label _amountLabel;
        private const double _minAmount = 5;
 
        public DonatePage(int orgId, int campaignId, IEnumerable<BankAccount> bankAccounts)
        {
            _orgId = orgId;
            _campaignId = campaignId;
            _bankAccounts = bankAccounts;

            var bankAccountDictionary = _bankAccounts.Select((x, i) => new {Value = x, Index = i}).ToDictionary(b => b.Index.ToString(), b => b.Value);

            var viewModel = new DonateModalViewModel(Navigation, _orgId, _campaignId, bankAccountDictionary);
            BindingContext = viewModel;

            var layout = new StackLayout();

            var headerLabel = new Label
            {
                Text = "Donate"
            };
            layout.Children.Add(headerLabel);

            _amountLabel = new Label
            {
                Text = String.Format("Donate: {0}", _minAmount.ToString("C", CultureInfo.CurrentCulture))
            };
            layout.Children.Add(_amountLabel);

            var amount = new Stepper
            {
                Maximum = 5000,
                Minimum = _minAmount,
                Increment = 0.5
            };

            amount.SetBinding(Stepper.ValueProperty, DonateModalViewModel.AmountPropertyName );
            amount.Value = _minAmount;
            amount.ValueChanged += async (sender, e) =>
            {
                _amountLabel.Text = String.Format("Donate: {0}", e.NewValue.ToString("C", CultureInfo.CurrentCulture));
            };
            layout.Children.Add(amount);

            var account = new Picker
            {
                Title = "Payment Type"
            };
            foreach (var b in _bankAccounts.Where(b => b.Status == "Verified"))
            {
                account.Items.Add(b.BankAccountName);
            }
            account.SetBinding(Picker.SelectedIndexProperty, DonateModalViewModel.AccountIdPropertyName);
            layout.Children.Add(account);

            var button = new Button
            {
                Text = "Donate"
            };
            button.SetBinding(Button.CommandProperty, DonateModalViewModel.DonateCommandPropertyName);
            layout.Children.Add(button);

            Content = new ScrollView
            {
                Content = layout
            };
        }
    }
}
