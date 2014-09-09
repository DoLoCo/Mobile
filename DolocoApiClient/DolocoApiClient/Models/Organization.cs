using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolocoApiClient.Models
{
    public class Organization : IDolocoEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
