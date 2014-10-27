using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.Helpers
{
    public class UserAvatarConverter : IValueConverter
    {
        private const string _defaultImgUrl = "http://www.gravatar.com/avatar/00000000000000000000000000000000?d=retro";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var avatar = (string) value;

            return ImageSource.FromUri(!string.IsNullOrEmpty(avatar) ? new Uri(avatar) : new Uri(_defaultImgUrl));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
