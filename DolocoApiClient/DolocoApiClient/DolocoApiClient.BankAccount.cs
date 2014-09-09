using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using DolocoApiClient.Network;
using DolocoApiClient.Payloads;

namespace DolocoApiClient
{
    public partial class DolocoApiClient
    {
        public Task<IEnumerable<BankAccount>> GetBankAccountsAsync()
        {
            var bankAccountUrl = GetRoutePathUrl(DolocoApiRouteEnum.BankAccounts);

            return _client.GetAsync<BankAccountsPayload>(bankAccountUrl).Process(payload =>
            {
                var bankAccounts = payload.BankAccounts.ToList();

                return bankAccounts.AsEnumerable();
            });
        }

        public Task<BankAccount> GetBankAccountAsyng(int bankAccountId)
        {
            var bankAccountUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.BankAccount), bankAccountId);

            return _client.GetAsync<BankAccount>(bankAccountUrl).Process();
        }

        public Task<BankAccount> CreateBankAccountAsync(string accountNumber, string accountType, string routingNumber,
            string accountName)
        {
            var bankAccountUrl = GetRoutePathUrl(DolocoApiRouteEnum.BankAccounts);
            var bankAccountPayload = new Dictionary<string, string>
            {
                {"account_number", accountNumber},
                {"account_type", accountType},
                {"account_name", accountName},
                {"routing_number", routingNumber}
            };

            return _client.PostAsync<BankAccount>(bankAccountUrl, bankAccountPayload).Process();
        }

        public Task<BankAccount> DeleteBankAccountAsync(int bankAccountId)
        {
            var bankAccountUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.BankAccount), bankAccountId);

            return _client.DeleteAsync<BankAccount>(bankAccountUrl).Process();
        }
    }
}
