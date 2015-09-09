using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using Doloco;
using MonoTouch.UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;


//[assembly: ExportRenderer(typeof(NavigationRenderer), typeof(BlueNavigationRenderer))]
namespace Doloco.iOS.Renderers
{
  public class BlueNavigationRenderer : NavigationRenderer
  {
    public BlueNavigationRenderer()
    {
      


    }
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      this.NavigationBar.TintColor = UIColor.White;
      this.NavigationBar.BarTintColor = Helpers.Color.Blue.ToFormsColor().ToUIColor();
      this.NavigationBar.BarStyle = UIBarStyle.Black;
    }
  }
}