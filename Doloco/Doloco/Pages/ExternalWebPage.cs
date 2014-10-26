using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class ExternalWebPage: ContentPage
    {
        public ExternalWebPage(string webPageUri)
        {
            var webPageView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = webPageUri,
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Content = new StackLayout
            {
                Children =
		        {
		            webPageView
		        }
            };
        }
    }
}
