using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyCore.Models.Store
{
    public class StoreMoveBill_MX
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 主表主键
        /// </summary>
        public int Bill_id { get; set; }
        /// <summary>
        /// 出库仓库ID
        /// </summary>
        public int? OutStroeInfo_id { get; set; }
        /// <summary>
        /// 出库仓库名称
        /// </summary>
        [StringLength(45)]
        public string OutStoreName { get; set; }
        /// <summary>
        /// 单据对应库存行号
        /// </summary>
        public int? StoreRow { get; set; }
        /// <summary>
        /// 入库仓库ID
        /// </summary>
        public int? InStroeInfo_id { get; set; }
        /// <summary>
        ///入库出仓库名称
        /// </summary>
        [StringLength(45)]
        public string InStoreName { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Good_id { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        [StringLength(45)]
        public string GoodID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [StringLength(145)]
        public string GoodName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(45)]
        public string DW { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [StringLength(45)]
        public string GGType { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        [StringLength(45)]
        public string ModelType { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        [StringLength(145)]
        public string SCCJ { get; set; }
        /// <summary>
        /// 调拨数量
        /// </summary>
        public decimal Num { get; set; }
        /// <summary>
        /// 进货价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 生产批号
        /// </summary>
        [StringLength(100)]
        public string SCPH { get; set; }
        /// <summary>
        /// 灭菌批号
        /// </summary>
        [StringLength(100)]
        public string MJPH { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>   
        public DateTime? scDate { get; set; }
        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime? yxqDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string BZ { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("Bill_id")]
        public virtual StoreMoveBill StoreMoveBill { get; set; }
    }
}
