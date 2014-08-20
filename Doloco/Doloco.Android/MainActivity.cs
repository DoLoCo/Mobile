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
    [Activity(Label = "Doloco", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            RaygunClient.Attach("i27YVEgFvRzBI8gZoIeMkg==");
            base.OnCreate(bundle);

            App.Init(typeof(App).Assembly);
            Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);

            // Set our view from the "main" layout resource
            SetPage(BuildView());
        }

        static Page BuildView()
        {
            return new NavigationPage(new LoginPage());
        }
    }
}

