using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Doloco.Views;
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

            this.Title = "Donate";

            _amountLabel = new Label
            {
                Text = String.Format("Donate: {0}", _minAmount.ToString("C", CultureInfo.CurrentCulture)),
                Font = Font.SystemFontOfSize(40, FontAttributes.Bold),
                TextColor = Color.FromHex("f79848"),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            layout.Children.Add(_amountLabel);

            var amount = new Slider
            {
                Maximum = 5000,
                Minimum = _minAmount
            };

            amount.ValueChanged += async (sender, e) =>
            {
                _amountLabel.Text = String.Format("Donate: {0}", e.NewValue.ToString("C", CultureInfo.CurrentCulture));
                viewModel.DonationAmount = e.NewValue;
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

            var button = new DefaultButton
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
