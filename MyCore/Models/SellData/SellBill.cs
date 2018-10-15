using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCore.Models.SellData
{
    public class SellBill
    {
        /// <summary>
        /// 销售单编号
        /// </summary>
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
        /// 单据类型SL销售开单。SR销售退货
        /// </summary>
        [StringLength(10)]
        public string BillType { get; set; }
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
        /// 销售员ID
        /// </summary>
        public int SellNameID { get; set; }
        /// <summary>
        /// 销售员
        /// </summary>
        [StringLength(45)]
        public string SellName { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int Sup_id { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [StringLength(145)]
        public string SupName { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int StroeInfo_id { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        [StringLength(45)]
        public string StoreName { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? Sum { get; set; }
        /// <summary>
        /// 其他成本/快递费等等
        /// </summary>
        public decimal? GiveSum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string BZ { get; set; }
        /// <summary>
        /// 单据状态 0草稿 1完成
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 完成日期
        /// </summary>
        public DateTime? SHDate { get; set; }
        /// <summary>
        /// 完成人
        /// </summary>
        [StringLength(45)]
        public string SHName { get; set; }
        /// <summary>
        /// 明细表
        /// </summary>
        public virtual List<SellBill_MX> SellBill_MX { get; set; }
    }
}
