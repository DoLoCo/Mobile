using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolocoApiClient.Models
{
    public class BankAccount : IDolocoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GatewayInterfaceId { get; set; }
        public string Status { get; set; }
        public string LastFour { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
