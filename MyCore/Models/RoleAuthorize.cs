using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCore.Models
{
    public class RoleAuthorize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        
        public int RoleID { get; set; }

        [StringLength(45)]
        public string MenuID { get; set; }

        public int ? Sort { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(45)]
        public string CreateName { get; set; }

    }
}
