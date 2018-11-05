using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.DAL;
using MyCore.Models.BaseData;
using MyCore.Models.CGData;
using MyCore.Models.Store;
using MyCore.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MyCore.Models.Search;

namespace MyCore.Controllers.Store
{
    public class GoodsStoreController : BaseController
    {
        private MyCoreContext conn;

        public GoodsStoreController(MyCoreContext _conn)
        {
            conn = _conn;
        }
        public IActionResult GoodsStoreIndex()
        {
            return View();
        }

        public async Task<IActionResult> GoodsStoreList(string sidx, string sord, int page, int rows, Search_GoodStoreBill Search )
        {
            Expression<Func<GoodsStore, bool>> predicate = ExpressionBuilder.True<GoodsStore>();
            predicate = predicate.And(b => b.Num > 0);

            if (Search.StartDate != null)
            {
                predicate = predicate.And(b => b.yxqDate >= Search.StartDate);
            }
            if (Search.EndDate != null)
            {
                predicate = predicate.And(b => b.yxqDate <= Search.EndDate);
            }
            if (!string.IsNullOrWhiteSpace(Search.StoreName))
            {
                predicate = predicate.And(b => b.StoreName.Contains(Search.StoreName));
            }
            if (!string.IsNullOrWhiteSpace(Search.GoodID))
            {
                predicate = predicate.And(b => b.GoodID.Contains(Search.GoodID));
            }
            if (!string.IsNullOrWhiteSpace(Search.GoodName))
            {
                predicate = predicate.And(b => b.GoodName.Contains(Search.GoodName));
            }
            if (!string.IsNullOrWhiteSpace(Search.DW))
            {
                predicate = predicate.And(b => b.DW.Contains(Search.DW));
            }
            if (!string.IsNullOrWhiteSpace(Search.GGType))
            {
                predicate = predicate.And(b => b.GGType.Contains(Search.GGType));
            }
            if (!string.IsNullOrWhiteSpace(Search.ModelType))
            {
                predicate = predicate.And(b => b.ModelType.Contains(Search.ModelType));
            }
            if (!string.IsNullOrWhiteSpace(Search.SCCJ))
            {
                predicate = predicate.And(b => b.SCCJ.Contains(Search.SCCJ));
            }
            if (!string.IsNullOrWhiteSpace(Search.SCPH))
            {
                predicate = predicate.And(b => b.SCPH.Contains(Search.SCPH));
            }
            if (!string.IsNullOrWhiteSpace(Search.MJPH))
            {
                predicate = predicate.And(b => b.MJPH.Contains(Search.MJPH));
            }

            IQueryable<GoodsStore> bills = conn.GoodsStore.Where(predicate);

          

            var lists = await bills.ToListAsync();
            return lists.GetJson<GoodsStore>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<GoodsStore>());
        }

        [HttpPost]
        public async Task<IActionResult> GetFile(string JsonSearch)
        {
            //json字符串转模型
            var Search = SysTool.JsonToModel<Search_GoodStoreBill>(JsonSearch);

            Expression<Func<GoodsStore, bool>> predicate = ExpressionBuilder.True<GoodsStore>();
            predicate = predicate.And(b => b.Num > 0);

            if (Search.StartDate != null)
            {
                predicate = predicate.And(b => b.yxqDate >= Search.StartDate);
            }
            if (Search.EndDate != null)
            {
                predicate = predicate.And(b => b.yxqDate <= Search.EndDate);
            }
            if (!string.IsNullOrWhiteSpace(Search.StoreName))
            {
                predicate = predicate.And(b => b.StoreName.Contains(Search.StoreName));
            }
            if (!string.IsNullOrWhiteSpace(Search.GoodID))
            {
                predicate = predicate.And(b => b.GoodID.Contains(Search.GoodID));
            }
            if (!string.IsNullOrWhiteSpace(Search.GoodName))
            {
                predicate = predicate.And(b => b.GoodName.Contains(Search.GoodName));
            }
            if (!string.IsNullOrWhiteSpace(Search.DW))
            {
                predicate = predicate.And(b => b.DW.Contains(Search.DW));
            }
            if (!string.IsNullOrWhiteSpace(Search.GGType))
            {
                predicate = predicate.And(b => b.GGType.Contains(Search.GGType));
            }
            if (!string.IsNullOrWhiteSpace(Search.ModelType))
            {
                predicate = predicate.And(b => b.ModelType.Contains(Search.ModelType));
            }
            if (!string.IsNullOrWhiteSpace(Search.SCCJ))
            {
                predicate = predicate.And(b => b.SCCJ.Contains(Search.SCCJ));
            }
            if (!string.IsNullOrWhiteSpace(Search.SCPH))
            {
                predicate = predicate.And(b => b.SCPH.Contains(Search.SCPH));
            }
            if (!string.IsNullOrWhiteSpace(Search.MJPH))
            {
                predicate = predicate.And(b => b.MJPH.Contains(Search.MJPH));
            }
            IQueryable<GoodsStore> bills = conn.GoodsStore.Where(predicate);
            var lists = await bills.ToListAsync();
            byte[] buffer = ExcelHelp.Export<GoodsStore>(lists, "库存信息", "库存信息", SysTool.GetPropertyNameArray<GoodsStore>()).GetBuffer();
            var fileName = "库存信息" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            return File(buffer, "application/vnd.ms-excel", fileName);

        }
    }
}