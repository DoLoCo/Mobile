using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.Views
{
    public class DefaultButton:Button
    {
        public DefaultButton()
        {
            this.BackgroundColor = Color.FromHex("4f81bc");
            this.BorderColor = Color.Transparent;
            this.TextColor = Color.White;
            this.BorderRadius = 0;
        }
    }
}
