using Doloco.iOS;
using Doloco.Views;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using MonoTouch.UIKit;

[assembly: ExportCell (typeof (DarkTextCell), typeof (DarkTextCellRenderer))]

namespace Doloco.iOS
{

    public class DarkTextCellRenderer : ImageCellRenderer
    {
        public override UITableViewCell GetCell (Cell item, UITableView tv)
        {
            var cellView = base.GetCell (item, tv);

            cellView.BackgroundColor = Color.Transparent.ToUIColor();
            cellView.TextLabel.TextColor = Color.FromHex("FFFFFF").ToUIColor();
            cellView.DetailTextLabel.TextColor = Color.FromHex("AAAAAA").ToUIColor();

            tv.SeparatorColor = Color.FromHex("444444").ToUIColor();

            return cellView;
        }
    }
    
}
