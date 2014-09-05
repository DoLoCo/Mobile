using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class CampaignsPage:ContentPage
    {
        public CampaignsPage()
        {
            var viewModel = new CampaignsViewModel(Navigation);
            BindingContext = viewModel;

            var btn = new Button();

            var list = new ListView { ItemsSource = viewModel.Model };

            var stack = new StackLayout();
            stack.Children.Add(list);
            Content = stack;
        }
    }
}
