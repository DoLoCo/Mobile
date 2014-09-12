using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class HomePage : TabbedPage
    {
        public HomePage()
        {
            var mapPage = new CampaignMapPage
            {
                Title = "Map"
            };
            Children.Add(mapPage);

            Children.Add(new ContentPage
            {
                Title = "List",
                Content = new StackLayout
                {
                    Children = {
                    new BoxView { Color = Color.Blue },
                    new BoxView { Color = Color.Red}
                }
                }
            });
        }

        public ContentPage MapPage()
        {
            return new ContentPage();
        }
    }
}
