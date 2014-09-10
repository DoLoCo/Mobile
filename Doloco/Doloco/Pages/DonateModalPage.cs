using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using DolocoApiClient.Models;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class DonateModalPage:ContentPage
    {
        private readonly int _orgId;
        private readonly int _campaignId;
        private readonly IEnumerable<BankAccount> _bankAccounts;
        private readonly Label _amountLabel;
        private const double _minAmount = 5;
 
        public DonateModalPage(int orgId, int campaignId, IEnumerable<BankAccount> bankAccounts)
        {
            _orgId = orgId;
            _campaignId = campaignId;
            _bankAccounts = bankAccounts;

            var bankAccountDictionary = _bankAccounts.Select((x, i) => new {Value = x, Index = i}).ToDictionary(b => b.Index.ToString(), b => b.Value);

            var viewModel = new DonateModalViewModel(Navigation, _orgId, _campaignId, bankAccountDictionary);
            BindingContext = viewModel;

            var layout = new StackLayout { Padding = 10 };

            var headerLabel = new Label
            {
                Text = "Donate"
            };
            layout.Children.Add(headerLabel);

            _amountLabel = new Label
            {
                Text = String.Format("Donate: ${0:F0}", _minAmount)
            };
            layout.Children.Add(_amountLabel);

            var amount = new Slider
            {
                Maximum = 5000,
                Minimum = _minAmount
            };

            amount.SetBinding(Slider.ValueProperty, DonateModalViewModel.AmountPropertyName );
            amount.ValueChanged += async (sender, e) =>
            {
                _amountLabel.Text = String.Format("Donate: ${0:F0}", e.NewValue);
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
            layout.Children.Add(account);

            var button = new Button
            {
                Text = "Donate",
                BackgroundColor = Helpers.Color.Blue.ToFormsColor()
            };
            button.SetBinding(Button.CommandProperty, DonateModalViewModel.DonateCommandPropertyName);
            layout.Children.Add(button);

            Content = new ScrollView { Content = layout };
        }
    }
}
