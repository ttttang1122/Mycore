using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyCore.Models.BaseData
{
    public class Goodinfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
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
        /// 通用名
        /// </summary>
        [StringLength(145)]
        public string TYName { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        [StringLength(145)]
        public string PYM { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        [StringLength(100)]
        public string TXM { get; set; }
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
        /// 许可证
        /// </summary>
        [StringLength(145)]
        public string XKZ { get; set; }
        /// <summary>
        /// 大纲类别
        /// </summary>
        [StringLength(45)]
        public string ClassID { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        [StringLength(45)]
        public string ForType { get; set; }
        /// <summary>
        /// 零售价
        /// </summary>
        public decimal ShopPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string BZ { get; set; }
        /// <summary>
        /// 状态1 正常 2删除
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [StringLength(45)]
        public string CreateName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? EditDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [StringLength(45)]
        public string EditName { get; set; }


    }
}
