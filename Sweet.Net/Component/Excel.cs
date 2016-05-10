using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sweet.Net.Component
{
    public class Excel
    {
        /// <summary>
        /// 保存数据列表到Excel（泛型）
        /// </summary>
        /// <typeparam name="T">集合数据类型</typeparam>
        /// <param name="data">数据列表</param>
        /// <param name="FileName">Excel文件</param>
        /// <param name="OpenPassword">Excel打开密码</param>
        public static void SaveToExcel<T>(IEnumerable<T> data, string FileName, string OpenPassword = null)
        {
            FileInfo file = new FileInfo(FileName);
            try
            {
                using (ExcelPackage ep = new ExcelPackage(file, OpenPassword))
                {
                    ExcelWorksheet ws = ep.Workbook.Worksheets.Add(typeof(T).Name);
                    ws.Cells["A1"].LoadFromCollection(data, true, TableStyles.None);
                    ep.Save(OpenPassword);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <typeparam name="T">集合数据类型</typeparam>
        /// <param name="data">数据列表</param>
        /// <param name="FileName">Excel文件名</param>
        public static void Export<T>(IEnumerable<T> data, string FileName, string OpenPassword = null)
        {
            FileInfo file = new FileInfo(FileName);
            try
            {
                using (ExcelPackage ep = new ExcelPackage(file))
                {
                    ExcelWorksheet ws = ep.Workbook.Worksheets.Add(typeof(T).Name);
                    ws.Cells["A1"].LoadFromCollection(data, true, TableStyles.None);
                    var resp = HttpContext.Current.Response;
                    ep.SaveAs(resp.OutputStream, OpenPassword);
                    resp.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    resp.AddHeader("content-disposition", "attachment;  filename=" + FileName + ".xls");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 从Excel中加载数据（泛型）
        /// </summary>
        /// <typeparam name="T">每行数据的类型</typeparam>
        /// <param name="FileName">Excel文件名</param>
        /// <returns>泛型列表</returns>
        public static IEnumerable<T> LoadFromExcel<T>(string FileName, string OpenPassword = null) where T : new()
        {
            FileInfo existingFile = new FileInfo(FileName);
            List<T> resultList = new List<T>();
            Dictionary<string, int> dictHeader = new Dictionary<string, int>();

            using (ExcelPackage package = new ExcelPackage(existingFile, OpenPassword))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                int colStart = worksheet.Dimension.Start.Column;  //工作区开始列
                int colEnd = worksheet.Dimension.End.Column;       //工作区结束列
                int rowStart = worksheet.Dimension.Start.Row;       //工作区开始行号
                int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

                //将每列标题添加到字典中
                for (int i = colStart; i <= colEnd; i++)
                {
                    dictHeader[worksheet.Cells[rowStart, i].Value.ToString()] = i;
                }

                List<PropertyInfo> propertyInfoList = new List<PropertyInfo>(typeof(T).GetProperties());

                for (int row = rowStart + 1; row <= rowEnd; row++)
                {
                    T result = new T();

                    //为对象T的各属性赋值
                    foreach (PropertyInfo p in propertyInfoList)
                    {
                        ExcelRange cell = worksheet.Cells[row, dictHeader[p.Name]]; //与属性名对应的单元格
                        if (cell.Value == null) continue;
                        var res = GetProertyInfo(result, cell, p);
                        if (res == null) continue;
                        result = res;
                    }
                    resultList.Add(result);
                }
            }
            return resultList;
        }

        private static T GetProertyInfo<T>(T result, ExcelRange cell, PropertyInfo p)
        {
            try
            {
                switch (p.PropertyType.Name.ToLower())
                {
                    case "string":
                        p.SetValue(result, cell.GetValue<String>());
                        break;
                    case "int16":
                        p.SetValue(result, cell.GetValue<Int16>());
                        break;
                    case "int32":
                        p.SetValue(result, cell.GetValue<Int32>());
                        break;
                    case "int64":
                        p.SetValue(result, cell.GetValue<Int64>());
                        break;
                    case "decimal":
                        p.SetValue(result, cell.GetValue<Decimal>());
                        break;
                    case "double":
                        p.SetValue(result, cell.GetValue<Double>());
                        break;
                    case "datetime":
                        p.SetValue(result, cell.GetValue<DateTime>());
                        break;
                    case "boolean":
                        p.SetValue(result, cell.GetValue<Boolean>());
                        break;
                    case "byte":
                        p.SetValue(result, cell.GetValue<Byte>());
                        break;
                    case "char":
                        p.SetValue(result, cell.GetValue<Char>());
                        break;
                    case "single":
                        p.SetValue(result, cell.GetValue<Single>());
                        break;
                    default:
                        break;
                }
                return result;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
