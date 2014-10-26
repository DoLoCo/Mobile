using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Doloco.Droid.Renderers;
using Doloco.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]

namespace Doloco.Droid.Renderers
{
    public class RoundedEntryRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var entry = (EditText) Control;
                entry.SetBackgroundResource(2130837786);
                entry.SetHintTextColor(Color.White.ToAndroid());
                entry.SetTypeface(Typeface.SansSerif, TypefaceStyle.Normal);
            }
        }

    }
}