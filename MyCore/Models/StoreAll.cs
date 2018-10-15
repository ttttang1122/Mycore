using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCore.Models
{
    public class StoreAll
    {
     
        public int id { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public int Store_id { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [StringLength(45)]
        public string StoreName { get; set; }
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
        /// 进单价
        /// </summary>
        public decimal InPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SellPrice { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal Num { get; set; }
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
    }
}
