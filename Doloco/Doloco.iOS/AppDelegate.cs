using System;
using System.Collections.Generic;
using System.Linq;
using Mindscape.Raygun4Net;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;

namespace Doloco.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate, ILoginManager
    {
        private UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            RaygunClient.Attach("i27YVEgFvRzBI8gZoIeMkg==");

            window = new UIWindow(UIScreen.MainScreen.Bounds);

            window.RootViewController = App.GetLoginPage(this).CreateViewController();
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
    }
}