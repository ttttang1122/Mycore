using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCore.Models
{
    public class View_User
    {
        public int id { get; set; }

        [StringLength(45)]
        public string UserID { get; set; }

        [StringLength(45)]
        public string UserName { get; set; }

        [StringLength(45)]
        public string LoginName { get; set; }

        [StringLength(45)]
        public string LoginPWD { get; set; }


        public int? RID{ get; set; }


        public int? OID { get; set; }

        [StringLength(45)]
        public string Sex { get; set; }

        [StringLength(45)]
        public string Phone { get; set; }

        [StringLength(145)]
        public string BZ { get; set; }

        [StringLength(45)]
        public string Status { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(45)]
        public string CreateName { get; set; }

        public DateTime? EditDate { get; set; }

        [StringLength(45)]
        public string EditName { get; set; }

        [StringLength(45)]
        public string RoleName { get; set; }

        [StringLength(45)]
        public string RoleType { get; set; }

        [StringLength(45)]
        public string OfficeName { get; set; }

        [StringLength(45)]
        public string FZName { get; set; }

    }
}
