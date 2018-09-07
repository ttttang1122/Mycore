using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyCore.Models
{
    public class Office
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
 
        [StringLength(45)]
        public string OfficeID { get; set; }

        [StringLength(45)]
        public string ParentID { get; set; }

        [StringLength(45)]
        public string OfficeName { get; set; }

        [StringLength(45)]
        public string FZName { get; set; }

        [StringLength(200)]
        public string BZ { get; set; }

        [StringLength(45)]
        public string Status { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(45)]
        public string CreateName { get; set; }

        public DateTime? EditeDate { get; set; }

        [StringLength(45)]
        public string EditName { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
