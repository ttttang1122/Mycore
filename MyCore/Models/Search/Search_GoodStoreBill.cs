using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCore.Models.Search
{
    public class Search_GoodStoreBill
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StoreName { get; set; }
        public string GoodID { get; set; }
        public string GoodName { get; set; }
        public string DW { get; set; }
        public string GGType { get; set; }
        public string ModelType { get; set; }
        public string SCCJ { get; set; }
        public string SCPH { get; set; }
        public string MJPH { get; set; }
    }
}
