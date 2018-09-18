using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCore.Models.CGData
{
    public class OrderBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        [StringLength(45)]
        public string BillID { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime? BillDate { get; set; }
        /// <summary>
        /// 制单人日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        [StringLength(45)]
        public string CreateName { get; set; }
        /// <summary>
        /// 采购员ID
        /// </summary>
        public int CGNameID { get; set; }
        /// <summary>
        /// 采购员
        /// </summary>
        [StringLength(45)]
        public string  CGName { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        public int Sup_id { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        [StringLength(145)]
        public string SupName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string BZ { get; set; }
        /// <summary>
        /// 完成状态 0未完成  1已完成
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int? SHStatus { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? SHDate { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [StringLength(45)]
        public string SHName { get; set; }

        public virtual List<OrderBill_MX> OrderBill_MX { get; set; }

    }
}
