﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Doloco.Views;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class VerifyBankPage:ContentPage
    {
        public VerifyBankPage(int accountId)
        {
            var viewModel = new VerifyBankViewModel(Navigation, accountId);
            BindingContext = viewModel;

            var layout = new StackLayout();

            var amountOne = new Entry { Placeholder = "First Amount" };
            amountOne.SetBinding(Entry.TextProperty, VerifyBankViewModel.AmountOnePropertyName);
            layout.Children.Add(amountOne);

            var amountTwo = new Entry { Placeholder = "Second Amount" };
            amountTwo.SetBinding(Entry.TextProperty, VerifyBankViewModel.AmountTwoPropertyName);
            layout.Children.Add(amountTwo);

            var verifyButton = new DefaultButton
            {
                Text = "VERIFY"
            };
            verifyButton.SetBinding(Button.CommandProperty, VerifyBankViewModel.AddCommandPropertyName);
            layout.Children.Add(verifyButton);

            Content = layout;
        }
    }
}
