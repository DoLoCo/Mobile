using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.Models;
using Doloco.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Doloco.Pages
{
    public class MasterPage<T>: ContentPage where T: class, new()
    {

        public MasterPage(OptionItem menuItem)
        {
            var viewModel = new ContentPage();
            BindingContext = viewModel;

            this.SetValue(Page.TitleProperty, menuItem.Title);
            this.SetValue(Page.IconProperty, menuItem.Icon);
        }
    }
}
