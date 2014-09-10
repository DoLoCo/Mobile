using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Xamarin.Forms;
using DolocoApiClient.Models;

namespace Doloco.ViewModel
{
	public class CreateCircleViewModel:BaseViewModel
	{
		private readonly INavigation _navigation;
		public IEnumerable Model;

		public CreateCircleViewModel(INavigation navigation)
		{
			_navigation = navigation;
		}

        public const string OrganizationNamePropertyName = "OrganizationName";
        private string _organizationName = string.Empty;
        public string OrganizationName
        {
            get { return _organizationName; }
            set { SetProperty(ref _organizationName, value, OrganizationNamePropertyName); }
        }

        public const string PhoneNumberPropertyName = "PhoneNumber";
        private string _phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value, PhoneNumberPropertyName); }
        }

        public const string DescriptionPropertyName = "Description";
        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value, DescriptionPropertyName); }
        }

        public const string AddressPropertyName = "Address";
        private string _address = string.Empty;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value, AddressPropertyName); }
        }

        public const string CityPropertyName = "City";
        private string _city = string.Empty;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value, CityPropertyName); }
        }

        public const string StatePropertyName = "State";
        private string _state = string.Empty;
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value, StatePropertyName); }
        }

        public const string PostalCodePropertyName = "PostalCode";
        private string _postalCode = string.Empty;
        public string PostalCode
        {
            get { return _postalCode; }
            set { SetProperty(ref _postalCode, value, PostalCodePropertyName); }
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
                var circle = await
                    App.ApiClient.CreateOrganizationAsync(_organizationName, _phoneNumber, _description, _address, _city,
                        _state,
                        _postalCode);

                var circleBankAccountPage = new CreateCircleBankAccountPage(circle.Id);

                await _navigation.PushAsync(circleBankAccountPage);
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }
        }
	}
}

