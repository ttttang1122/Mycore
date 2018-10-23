using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCore.Models.Search
{
    public class MXBillSearch
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string GoodName { get; set; }
        public string GGType { get; set; }
        public string ModelType { get; set; }
        public string SCPH { get; set; }
        public string MJPH { get; set; }
        public string SCCJ { get; set; }
        public string SupName { get; set; }
    }
}
