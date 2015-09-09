using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DolocoApiClient.Models
{
    public class BankAccount : IDolocoEntity
    {
        public int Id { get; set; }
        [JsonProperty("bank_account_name")]
        public string BankAccountName { get; set; }
        public string GatewayInterfaceId { get; set; }
        public string Status { get; set; }
        [JsonProperty("last_four")]
        public string LastFour { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
