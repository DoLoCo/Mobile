using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Views;
using Android.Widget;
using Doloco.Pages;
using Mindscape.Raygun4Net;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Doloco.Droid
{
	[Activity(Label = "Doloco", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/_AppTheme")]
    public class MainActivity : AndroidActivity, ILoginManager, ILocationListener, IMediaPicker
    {
            
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            RaygunClient.Attach("");
            Insights.Initialize("", Application.Context);
			Forms.Init (this, bundle);
            FormsMaps.Init(this, bundle);

            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            InitializeLocationManager();
		    App.SetMediaPicker(this);

			SetPage (App.GetLoginPage (this));
		}

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.show_circle_list:
                    SetPage(new CirclesPage());
                    return true;
            }
            return base.OnOptionsItemSelected(item);
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
	    private Criteria _locationServiceCriteria;

	    public void InitializeLocationManager()
	    {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            _locationServiceCriteria = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(_locationServiceCriteria, true);

            _locationProvider = acceptableLocationProviders.Any() ? acceptableLocationProviders.First() : String.Empty;
	    }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

        protected override void OnResume()
        {
            base.OnResume();
            if (_locationManager.GetProviders(_locationServiceCriteria, true).Contains(LocationManager.GpsProvider))
                _locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, this);

            if (_locationManager.GetProviders(_locationServiceCriteria, true).Contains(LocationManager.NetworkProvider))
                _locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, 0, 0, this);
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

        #region IMediaPicker implementation

	    private string _imgPath;
	    public void InitializeMediaPicker()
	    {
	            Intent = new Intent();
                Intent.SetType("image/*");
                Intent.SetAction(Intent.ActionGetContent);
	            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), 1000);
	    }

	    public string GetImage()
	    {
	        InitializeMediaPicker();

	        return _imgPath;
	    }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode != 1000) || (resultCode != Result.Ok) || (data == null)) return;
            var uri = data.Data;
            _imgPath = GetPathToImage(uri);
        }

        private string GetPathToImage(Android.Net.Uri uri)
        {
            string path = null;
            // The projection contains the columns we want to return in our query.
            string[] projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
            using (ICursor cursor = ManagedQuery(uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
            }
            return path;
        }
        #endregion
    }
}

