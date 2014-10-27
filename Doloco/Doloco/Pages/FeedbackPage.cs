using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class FeedbackPage:ContentPage
    {
        public FeedbackPage()
        {
            Device.OpenUri(new Uri("mailto:info@doloco.org"));
        }
    }
}
