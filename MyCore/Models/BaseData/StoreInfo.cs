using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyCore.Models.BaseData
{
    public class StoreInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [StringLength(45)]
        public string StoreName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(145)]
        public string Address { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        [StringLength(145)]
        public string Sizes { get; set; }
        /// <summary>
        /// 用途描述
        /// </summary>
        [StringLength(500)]
        public string UseSay { get; set; }       
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string BZ { get; set; }
        /// <summary>
        /// 状态
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
