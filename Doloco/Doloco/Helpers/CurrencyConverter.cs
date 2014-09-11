using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doloco.Helpers
{
    public struct CurrencyConverter
    {
        public static decimal FromCents(int amountInCents)
        {
            decimal decimalAmount = amountInCents/100;

            return decimalAmount;
        }
    }
}
