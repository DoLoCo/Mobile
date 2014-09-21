using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.Helpers
{
    public class BankAccountStatusIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (string) value;

            switch (status)
            {
                case "Verified":
                    return ImageSource.FromFile("credit50.png");
                case "Verification Failed":
                    return ImageSource.FromFile("disconnected.png");
                case "Verification Created":
                    return ImageSource.FromFile("bank10.png");
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
