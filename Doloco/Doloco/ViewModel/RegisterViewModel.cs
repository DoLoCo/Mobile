using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.Pages;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
    public class RegisterViewModel:BaseViewModel
    {
        private readonly INavigation _navigation;
        public RegisterViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        public const string EmailPropertyName = "Email";
        private string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value, EmailPropertyName); }
        }

        public const string FirstNamePropertyName = "Firstname";
        private string _firstName = string.Empty;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value, FirstNamePropertyName); }
        }

        public const string LastNamePropertyName = "Lastname";
        private string _lastName = string.Empty;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value, LastNamePropertyName); }
        }

        public const string PasswordPropertyName = "Password";
        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value, PasswordPropertyName); }
        }

        public const string PasswordConfirmPropertyName = "Passwordconfirm";
        private string _passwordConfirm = string.Empty;
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set { SetProperty(ref _passwordConfirm, value, PasswordConfirmPropertyName); }
        }

        private Command _registerCommand;
        public const string RegisterCommandPropertyName = "RegisterCommand";

        public Command RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new Command(async () => await ExecuteRegisterCommand()));
            }
        }

        protected async Task ExecuteRegisterCommand()
        {
            if (IsLoading)
                return;

            IsLoading = true;

            try
            {
                var client = new DolocoApiClient.DolocoApiClient("http://dolocony.asuscomm.com:3000/api/v1");

                var user = await client.RegisterUserAsync(_email, _firstName, _lastName, _password, _passwordConfirm);

                Debug.WriteLine(user.Email);
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", "Unable to load blog.", "OK", null);
            }

            IsLoading = false;
        }
    }
}
