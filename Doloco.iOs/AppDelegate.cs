using System;
using Doloco.Pages;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin;
using Xamarin.Forms;

namespace Doloco.iOs
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        private UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            App.Init(typeof(App).Assembly);
            Forms.Init();
            FormsMaps.Init();


            UINavigationBar.Appearance.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
            UINavigationBar.Appearance.BarTintColor = UIColor.White;
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                TextColor = UIColor.White
            });

            window.RootViewController = BuildView();
            window.MakeKeyAndVisible();

            return true;
        }

        static UIViewController BuildView()
        {
            var root = new NavigationPage(new LoginPage());
            var controller = root.CreateViewController();
            return controller;
        }
        //
        // This method is invoked when the application is about to move from active to inactive state.
        //
        // OpenGL applications should use this method to pause.
        //
        public override void OnResignActivation(UIApplication application)
        {
        }

        // This method should be used to release shared resources and it should store the application state.
        // If your application supports background exection this method is called instead of WillTerminate
        // when the user quits.
        public override void DidEnterBackground(UIApplication application)
        {
        }

        // This method is called as part of the transiton from background to active state.
        public override void WillEnterForeground(UIApplication application)
        {
        }

        // This method is called when the application is about to terminate. Save data, if needed. 
        public override void WillTerminate(UIApplication application)
        {
        }
    }
}