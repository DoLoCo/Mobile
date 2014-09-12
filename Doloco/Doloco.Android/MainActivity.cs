using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Doloco.Pages;
using Mindscape.Raygun4Net;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;

namespace Doloco.Droid
{
	[Activity(Label = "Doloco", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/_AppTheme")]
    public class MainActivity : AndroidActivity, ILoginManager, ILocationListener
    {
            
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            RaygunClient.Attach("i27YVEgFvRzBI8gZoIeMkg==");
			Forms.Init (this, bundle);
            FormsMaps.Init(this, bundle);

            InitializeLocationManager();

			SetPage (App.GetLoginPage (this));
		}

        #region ILoginManager implementation

        public void ShowMainPage()
        {
            SetPage(App.GetMainPage());
        }

        public void Logout()
        {
            SetPage(App.GetLoginPage(this));
        }
        #endregion

        #region ILocationManager implementation
        private String _locationProvider;
        private Location _currentLocation;
        private LocationManager _locationManager;

	    public void InitializeLocationManager()
	    {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = String.Empty;
            }
	    }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                App.UserLatitude = 0;
                App.UserLongitude = 0;
            }
            else
            {
                App.UserLatitude = _currentLocation.Latitude;
                App.UserLongitude = _currentLocation.Longitude;
            }
        }
        #endregion
    }
}

