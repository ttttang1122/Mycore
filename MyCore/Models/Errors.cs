using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCore.Models
{
    public class Errors
    {
       
       public string RequestId { get; set; }

       public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        
    }
}
