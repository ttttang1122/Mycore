using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCore.Models.Store
{
    public class StoreMoveBill
    {
        /// <summary>
        /// 仓库盘点单
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
        /// 制单人日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        [StringLength(45)]
        public string CreateName { get; set; }
        /// <summary>
        /// 经手人ID
        /// </summary>
        public int YSNameID { get; set; }
        /// <summary>
        /// 经手人
        /// </summary>
        [StringLength(45)]
        public string YSName { get; set; }
        /// <summary>
        /// 出仓库ID
        /// </summary>
        public int OutStroeInfo_id { get; set; }
        /// <summary>
        /// 出仓库名称
        /// </summary>
        [StringLength(45)]
        public string OutStoreName { get; set; }
        /// <summary>
        /// 入仓库ID
        /// </summary>
        public int InStroeInfo_id { get; set; }
        /// <summary>
        /// 入仓库名称
        /// </summary>
        [StringLength(45)]
        public string InStoreName { get; set; }       
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
        public virtual List<StoreMoveBill_MX> StoreMoveBill_MX { get; set; }
    }
}
