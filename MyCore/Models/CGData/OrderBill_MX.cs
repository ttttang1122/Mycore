﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyCore.Models.CGData
{
    public class OrderBill_MX
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 主表主键
        /// </summary>
        public int Bill_id { get; set; }
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
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Num { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Sum { get; set; }
        /// <summary>
        /// 完成数量
        /// </summary>
        public decimal EndNum { get; set; }
        /// <summary>
        /// 完成状态0未完成 1完成
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string BZ { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("Bill_id")]
        public virtual OrderBill OrderBill { get; set; }

    }
}
