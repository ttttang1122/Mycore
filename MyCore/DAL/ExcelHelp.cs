using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using MyCore.DAL;
using System.Reflection;

namespace MyCore.DAL
{
    public class ExcelHelp
    {
        public static MemoryStream Export<T>(List<T> lists,string strHead,string strSheetName,string[] ColumnNames)
        {
            string[] ListColumnName = SysTool.GetPropertyNameArray<T>();
            if (ColumnNames.Length != ListColumnName.Length)
            {
                return new MemoryStream();
            }

            #region 内容
            HSSFWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet(strSheetName);
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.ApplicationName = "NPOI";            //填加xls文件创建程序信息      
            si.LastAuthor = "后台系统";           //填加xls文件最后保存者信息      
            si.Comments = "后台系统自动创建文件";      //填加xls文件作者信息      
            si.Title = strHead;               //填加xls文件标题信息      
            si.Subject = strHead;              //填加文件主题信息      
            si.CreateDateTime = DateTime.Now;
            book.SummaryInformation = si;
            #endregion
            int rowIndex = 0;

            for (int j=0;j<lists.Count;j++)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = book.CreateSheet(strSheetName + ((int)rowIndex / 65535).ToString());
                    }

                    #region 表头及样式
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHead);
                        ICellStyle headStyle = book.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = book.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, ListColumnName.Length - 1));
                    }
                    #endregion


                    #region 列头及样式
                    {
                        //HSSFRow headerRow = sheet.CreateRow(1);   
                        IRow headerRow = sheet.CreateRow(1);

                        ICellStyle headStyle = book.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = book.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        for (int i = 0; i < ColumnNames.Length; i++)
                        {
                            headerRow.CreateCell(i).SetCellValue(ColumnNames[i]);
                            headerRow.GetCell(i).CellStyle = headStyle;
                            //设置列宽   
                            //sheet.SetColumnWidth(i, (arrColWidth[i] + 1) * 256);
                        }
                      
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                //foreach (DataColumn column in dtSource.Columns)   
                var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
                for (int i = 0; i < ListColumnName.Length; i++)
                {
                    ICell newCell = dataRow.CreateCell(i);
                    
                    //var pi = properties.First<PropertyInfo>(x => x.Name == ListColumnName[i]);//找到相同Name字段的2种方法都可以
                    var pr = properties[i];//顺序取的
                    var value = pr.GetValue(lists[j], null);
                    
                    string drValue = value != null ? value.ToString() : "";
                    newCell.SetCellValue(drValue);
                   
                }
                #endregion

                rowIndex++;
            }


            using (MemoryStream ms = new MemoryStream())
            {
                book.Write(ms);
                ms.Flush();
                ms.Position = 0;
                //sheet.Dispose();   
                sheet = null;
                book = null;
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet      
                return ms;
            }
        }
    }
}
