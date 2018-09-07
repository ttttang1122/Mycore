using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyCore.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }     

        [StringLength(45)]
        public string RoleID { get; set; }

        [StringLength(45)]
        public string RoleName { get; set; }

        [StringLength(45)]
        public string RoleType { get; set; }

        [StringLength(200)]
        public string BZ { get; set; }

        [StringLength(45)]
        public string Status { get; set; }


        public DateTime? CreateDate { get; set; }

        [StringLength(45)]
        public string CreateName { get; set; }

        public DateTime? EditDate { get; set; }

        [StringLength(45)]
        public string EditName { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
