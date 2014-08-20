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
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            BindingContext = new HomeViewModel(Navigation);
            var layout = new StackLayout();

            var label = new Label
            {
                Text = "Doloco",
                Font = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                XAlign = TextAlignment.Center, // Center the text in the blue box.
                YAlign = TextAlignment.Center, // Center the text in the blue box.
            };

            layout.Children.Add(label);

            Content = layout;
        }
    }
}
