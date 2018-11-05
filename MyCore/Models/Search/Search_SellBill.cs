using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCore.Models.Search
{
    public class Search_SellBill
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string BillID { get; set; }
        public string SupName { get; set; }
        public string JSName { get; set; }
        public string StoreName { get; set; }
    }
}
