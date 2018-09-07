using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.Models;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace MyCore.DAL
{
    /// <summary>
    /// 分页排序助手类
    /// </summary>
    public static class DataPagingHelper
    {
        public static IQueryable<T> GetQueryable<T>(this IList<T> list, string sidx, string sord, int page, int rows)
        {
            return GetQueryable<T>(list.AsQueryable<T>(), sidx, sord, page, rows);
        }

        public static IQueryable<T> GetQueryable<T>(this IQueryable<T> queriable, string sidx, string sord, int page, int rows)
        {
            var data = ApplyOrder<T>(queriable, sidx, sord.ToLower() == "asc" ? true : false);

            return data.Skip<T>((page - 1) * rows).Take<T>(rows);
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> queriable, string property, bool isASC)
        {
            PropertyInfo pi = typeof(T).GetProperty(property);
            ParameterExpression arg = Expression.Parameter(typeof(T), "x");
            Expression expr = Expression.Property(arg, pi);

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), pi.PropertyType);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            string methodName = isASC ? "OrderBy" : "OrderByDescending";

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), pi.PropertyType)
                    .Invoke(null, new object[] { queriable, lambda });

            return (IOrderedQueryable<T>)result;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class InternalAttribute : Attribute { }
    /// <summary>
    /// jqGrid数据处理助手类
    /// </summary>
    public static class JqGridHelper
    {
        public static JsonResult GetJson<T>(this IList<T> datas, string sidx, string sord, int page, int rows, params string[] fields)
        {
            return GetJson<T>(datas.AsQueryable<T>(), sidx, sord, page, rows, fields);
        }


        public static JsonResult GetJson<T>(this IQueryable<T> queriable, string sidx, string sord, int page, int rows,  params string[] fields)
        {
            var recordss = queriable.Count();
            var data = queriable.GetQueryable<T>(sidx, sord, page, rows);

          
            if (rows != 0)
            {
                var totalpages = (decimal)queriable.Count<T>() / (decimal)rows;
                totalpages = (totalpages == (int)totalpages) ? totalpages : (int)totalpages + 1;

                var rowsData = GetJsonData<T>(data, fields);

               var jsdata = new
                {

                    page,
                    records = recordss,
                    total = (int)totalpages,
                    rows = rowsData
                };
                return new JsonResult(jsdata);
            }
            return new JsonResult("");
      
        }


        private static object[] GetJsonData<T>(IQueryable<T> queriable, params string[] fields)
        {

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            T[] datas = queriable.ToArray<T>();

            object[] result = new object[datas.Length];


            if (fields.Length == 0)
            {
                fields = Array.ConvertAll<PropertyInfo, string>(properties.Where<PropertyInfo>
                    (x => x.GetCustomAttributes(typeof(InternalAttribute), false).Length == 0)
                    .ToArray<PropertyInfo>()
                    , delegate (PropertyInfo p)
                    {
                        return p.Name;
                    });
            }
            List<object> dic = new List<object>();
            for (int i = 0; i < datas.Length; i++)
            {

                Dictionary<string, object> results = new Dictionary<string, object>();
                object[] values = new object[fields.Length];
                for (int j = 0; j < fields.Length; j++)
                {


                    var pi = properties.First<PropertyInfo>(x => x.Name == fields[j]);

                    var value = pi.GetValue(datas[i], null);
                    values[j] = value != null ? value.ToString() : "";
                    results.Add(fields[j], values[j]);
                }
                dic.Add(results);
                // repeatitems: ture
                //result[i] = new { id = values[0], cell = values };
            }

            return dic.ToArray();
        }


    }
    public static class TreeSelect
    {
        public static string TreeSelectJson(this List<TreeSelectModel> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(TreeSelectJson(data, "0", ""));
            sb.Append("]");
            return sb.ToString();
        }
        private static string TreeSelectJson(List<TreeSelectModel> data, string parentId, string blank)
        {
            StringBuilder sb = new StringBuilder();
            var ChildNodeList = data.FindAll(t => t.parentId == parentId);
            var tabline = "";
            if (parentId != "0")
            {
                tabline = "　　";
            }
            if (ChildNodeList.Count > 0)
            {
                tabline = tabline + blank;
            }
            foreach (TreeSelectModel entity in ChildNodeList)
            {
                entity.text = tabline + entity.text;
                string strJson = entity.ToJson();
                sb.Append(strJson);
                sb.Append(TreeSelectJson(data, entity.id, tabline));
            }
            return sb.ToString().Replace("}{", "},{");
        }

        public static string TreeViewJson(this List<TreeViewModel> data, string parentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<TreeViewModel> item = data.FindAll(t => t.parentId == parentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (TreeViewModel entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"id\":\"" + entity.id + "\",");
                    strJson.Append("\"text\":\"" + entity.text.Replace("&nbsp;", "") + "\",");
                    strJson.Append("\"value\":\"" + entity.value + "\",");
                    if (entity.title != null && !string.IsNullOrEmpty(entity.title.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"title\":\"" + entity.title.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.img != null && !string.IsNullOrEmpty(entity.img.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"img\":\"" + entity.img.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.checkstate != null)
                    {
                        strJson.Append("\"checkstate\":" + entity.checkstate + ",");
                    }
                    if (entity.parentId != null)
                    {
                        strJson.Append("\"parentnodes\":\"" + entity.parentId + "\",");
                    }
                    strJson.Append("\"showcheck\":" + entity.showcheck.ToString().ToLower() + ",");
                    strJson.Append("\"isexpand\":" + entity.isexpand.ToString().ToLower() + ",");
                    if (entity.complete == true)
                    {
                        strJson.Append("\"complete\":" + entity.complete.ToString().ToLower() + ",");
                    }
                    strJson.Append("\"hasChildren\":" + entity.hasChildren.ToString().ToLower() + ",");
                    strJson.Append("\"ChildNodes\":" + TreeViewJson(data, entity.id) + "");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
