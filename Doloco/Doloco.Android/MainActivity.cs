using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Doloco.Pages;
using Mindscape.Raygun4Net;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Doloco.Droid
{
	[Activity(Label = "Doloco", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/_AppTheme")]
    public class MainActivity : AndroidActivity, ILoginManager
    {
            
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            RaygunClient.Attach("i27YVEgFvRzBI8gZoIeMkg==");

			Forms.Init (this, bundle);

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
    }
}

