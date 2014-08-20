using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Doloco.Pages;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        public LoginViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }
        public const string UsernamePropertyName = "Username";
        private string _username = string.Empty;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value, UsernamePropertyName); }
        }

        public const string PasswordPropertyName = "Password";
        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value, PasswordPropertyName); }
        }

        private Command _loginCommand;
        public const string LoginCommandPropertyName = "LoginCommand";
        public Command LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
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

        protected async Task ExecuteLoginCommand()
        {
            if (IsLoading)
                return;

            IsLoading = true;

            try
            {
                Debug.WriteLine(_username);
                Debug.WriteLine(_password);

                var token = await App.ApiClient.CreateSessionAsync(_username, _password);

                App.Token = token;

                await _navigation.PushAsync(new RootPage());
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }

            IsLoading = false;
        }

        protected async Task ExecuteRegisterCommand()
        {
            await _navigation.PushAsync(new RegisterPage());
        }
    }
}
