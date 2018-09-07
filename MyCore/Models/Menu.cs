using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyCore.Models
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(45)]
        public string MenuParentID { get; set; }

        [StringLength(45)]
        public string MenuID { get; set; }

        [StringLength(45)]
        public string MenuName { get; set; }

        [StringLength(45)]
        public string MenuNameCN { get; set; }

        [StringLength(100)]
        public string MenuUrl { get; set; }

        public int? MenuSort { get; set; }

        [StringLength(100)]
        public string MenuIcon { get; set; }

        public int? MenuType { get; set; }

        [StringLength(45)]
        public string BZ { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(45)]
        public string CreateName { get; set; }

    }
}
