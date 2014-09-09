using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;

namespace DolocoApiClient.Payloads
{
    internal class BankAccountsPayload
    {
        public IEnumerable<BankAccount> BankAccounts { get; set; } 
    }
}
