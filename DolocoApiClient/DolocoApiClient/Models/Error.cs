using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DolocoApiClient.Models
{
    [DataContract]
    public class Error
    {
        [DataMember(Name = "num")]
        public int Number { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
