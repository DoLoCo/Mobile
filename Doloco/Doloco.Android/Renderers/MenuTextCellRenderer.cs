using System;
using Doloco.Droid.Renderers;
using Doloco.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Widget;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics.Drawables;
using Android.Graphics;

using Color = Xamarin.Forms.Color;
using View = global::Android.Views.View;
using ViewGroup = global::Android.Views.ViewGroup;
using Context = global::Android.Content.Context;
using ListView = global::Android.Widget.ListView;
using Android.App;
using TextAlignment = Android.Views.TextAlignment;

[assembly: ExportCell (typeof (MenuTextCell), typeof (MenuTextCellRenderer))]

namespace Doloco.Droid.Renderers
{
    public class MenuTextCellRenderer : ImageCellRenderer
    {
        protected override View GetCellCore (Cell item, View convertView, ViewGroup parent, Context context)
        {
            var cell = (LinearLayout)base.GetCellCore (item, convertView, parent, context);
            cell.SetPadding(20, 10, 0, 10);
            cell.DividerPadding = 50;

            var div = new ShapeDrawable();
            div.SetIntrinsicHeight(1);
            div.Paint.Set(new Paint { Color = Color.FromHex("b7b7b7").ToAndroid() });

            if (parent is ListView)
            {
                ((ListView)parent).Divider = div;
                ((ListView)parent).DividerHeight = 1;
            }

            var icon = (ImageView)cell.GetChildAt(0);
            icon.SetMaxHeight(16);
            icon.SetMaxWidth(16);
            icon.SetScaleType(ImageView.ScaleType.CenterInside);
            icon.Layout(20,0,0,0);

            var label = (TextView)((LinearLayout)cell.GetChildAt(1)).GetChildAt(0);
            label.SetTextColor(Color.FromHex("262626").ToAndroid());
            label.TextSize = Font.SystemFontOfSize(NamedSize.Large).ToScaledPixel();
            label.TextAlignment = TextAlignment.Center;
            label.Text = label.Text.ToUpper();

            var secondaryLabel = (TextView)((LinearLayout)cell.GetChildAt(1)).GetChildAt(1);
            secondaryLabel.SetTextColor(Color.FromHex("262626").ToAndroid());
            secondaryLabel.TextSize = Font.SystemFontOfSize(NamedSize.Large).ToScaledPixel();
            label.TextAlignment = TextAlignment.Center;


            return cell;
        }
    }
}

