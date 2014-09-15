﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mindscape.Raygun4Net;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin;
using Xamarin.Forms;

namespace Doloco.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate, ILoginManager, ICLLocationManagerDelegate
    {
        private UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UIButton.Appearance.TintColor = UIColor.LightGray;
            UIButton.Appearance.SetTitleColor(UIColor.FromRGB(0, 127, 14), UIControlState.Normal);
            UISlider.Appearance.ThumbTintColor = UIColor.Red;
            UISlider.Appearance.MinimumTrackTintColor = UIColor.Orange;
            UISlider.Appearance.MaximumTrackTintColor = UIColor.Yellow;
            UIProgressView.Appearance.ProgressTintColor = UIColor.Yellow;
            UIProgressView.Appearance.TrackTintColor = UIColor.Orange;

            Forms.Init();
            FormsMaps.Init();
            RaygunClient.Attach("i27YVEgFvRzBI8gZoIeMkg==");
            InitializeLocationManager();

            window = new UIWindow(UIScreen.MainScreen.Bounds)
            {
                RootViewController = App.GetLoginPage(this).CreateViewController()
            };

            window.MakeKeyAndVisible();

            return true;
        }

        #region ILoginManager implementation

        // https://developer.apple.com/library/ios/documentation/WindowsViews/Conceptual/WindowAndScreenGuide/WindowScreenRolesinApp/WindowScreenRolesinApp.html
        public void ShowMainPage()
        {
            window.RootViewController = App.GetMainPage().CreateViewController();
            window.MakeKeyAndVisible();
        }

        public void Logout()
        {
            window.RootViewController = App.GetLoginPage(this).CreateViewController();
            window.MakeKeyAndVisible();
        }

        #endregion

        #region ICLLocationManager implementation

        private CLLocationManager _locationManager;
        public void InitializeLocationManager()
        {
            _locationManager = new CLLocationManager();

            if (CLLocationManager.LocationServicesEnabled)
            {
                _locationManager.StartMonitoringSignificantLocationChanges();
            }
            else
            {
                Console.WriteLine("Location services not enabled, please enable this in your Settings");
            }

            _locationManager.LocationsUpdated += (o, e) =>
            {
                var currentLocation = e.Locations.LastOrDefault();
                if (currentLocation != null)
                {
                    App.UserLatitude = currentLocation.Coordinate.Latitude;
                    App.UserLongitude = currentLocation.Coordinate.Longitude;
                }
                else
                {
                    App.UserLatitude = 0;
                    App.UserLongitude = 0;
                }
            };
        }
        #endregion
    }
}