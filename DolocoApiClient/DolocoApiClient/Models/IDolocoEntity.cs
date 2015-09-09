using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolocoApiClient.Models
{
    public interface IDolocoEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
