using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCore.Models.Search
{
    public class TakeMoneyTJBillcs
    {
        public int id { get; set; }
        /// <summary>
        /// 主表主键
        /// </summary>
        public int Bill_id { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int? StroeInfo_id { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 订单对应行号
        /// </summary>
        public int? OrderRow { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Good_id { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string DW { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string GGType { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string ModelType { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string SCCJ { get; set; }
        /// <summary>
        /// 进货价
        /// </summary>
        public decimal InPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SellPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Num { get; set; }
        /// <summary>
        /// 成本金额
        /// </summary>
        public decimal InSum { get; set; }
        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal SellSum { get; set; }
        /// <summary>
        /// 生产批号
        /// </summary>
        public string SCPH { get; set; }
        /// <summary>
        /// 灭菌批号
        /// </summary>
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
        public string BZ { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string BillID { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime? BillDate { get; set; }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string CreateName { get; set; }
        /// <summary>
        /// 采购员ID
        /// </summary>
        public int YSNameID { get; set; }
        /// <summary>
        /// 采购员
        /// </summary>
        public string YSName { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        public int Sup_id { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string SupName { get; set; }
        /// <summary>
        /// 单据类型IS入库。BR出库
        /// </summary>
        public string BillType { get; set; }
        /// <summary>
        /// 单据状态 0草稿 1完成
        /// </summary>
        public int? Status { get; set; }

    }
}
