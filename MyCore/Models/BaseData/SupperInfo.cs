using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCore.Models.BaseData
{
    public class SupperInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 客商编号
        /// </summary>
        [StringLength(45)]
        public string SupID { get; set; }
        /// <summary>
        /// 客商名称
        /// </summary>
        [StringLength(145)]
        public string SupName { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        [StringLength(145)]
        public string PYM { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(200)]
        public string Address { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(45)]
        public string FZName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(45)]
        public string Phone { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        [StringLength(45)]
        public string WeiXin { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        [StringLength(45)]
        public string dq { get; set; }
        /// <summary>
        /// 客商类别
        /// </summary>
        public int? SupType { get; set; }
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
