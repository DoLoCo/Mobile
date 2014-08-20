using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
    public class HomeViewModel:BaseViewModel
    {
        private readonly INavigation _navigation;
        public HomeViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }
    }
}
