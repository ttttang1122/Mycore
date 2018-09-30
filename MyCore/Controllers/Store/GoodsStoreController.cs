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

        public async Task<IActionResult> GoodsStoreList(string sidx, string sord, int page, int rows, string StrSearchType, string StrSearch)
        {
            IQueryable<GoodsStore> bills = conn.GoodsStore.Where(b=>b.Num>0);

            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            bills = bills.Where(b => b.StoreName.Contains(StrSearch));
                            break;
                        case "1":
                            bills = bills.Where(b => b.GoodID.Contains(StrSearch));

                            break;
                        case "2":
                            bills = bills.Where(b => b.GoodName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await bills.ToListAsync();
            return lists.GetJson<GoodsStore>(sidx, sord, page, rows, SysTool.GetPropertyNameArray<GoodsStore>());
        }

        [HttpPost]
        public async Task<IActionResult> GetFile(string StrSearchType, string StrSearch)
        {

            IQueryable<GoodsStore> bills = conn.GoodsStore.Where(b => b.Num > 0);

            if (!string.IsNullOrWhiteSpace(StrSearchType))
            {
                if (!string.IsNullOrWhiteSpace(StrSearch))
                {
                    switch (StrSearchType)
                    {
                        case "0":
                            bills = bills.Where(b => b.StoreName.Contains(StrSearch));
                            break;
                        case "1":
                            bills = bills.Where(b => b.GoodID.Contains(StrSearch));

                            break;
                        case "2":
                            bills = bills.Where(b => b.GoodName.Contains(StrSearch));

                            break;
                        default:

                            break;
                    }

                }
            }

            var lists = await bills.ToListAsync();

            byte[] buffer = ExcelHelp.Export<GoodsStore>(lists, "库存信息", "库存信息", SysTool.GetPropertyNameArray<GoodsStore>()).GetBuffer();


            var fileName = "库存信息" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

            return File(buffer, "application/vnd.ms-excel", fileName);
        }
    }
}