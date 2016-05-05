using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweet.Net.Ext
{
    public static class CollectionExt
    {
        /// <summary>
        /// 将dt2合并到dt中
        /// </summary>
        /// <param name="dt">被合并的DataTable</param>
        /// <param name="dt2">需要合并的DataTable</param>
        /// <returns>返回合并的DataTable</returns>
        public static DataTable AppendTable(this DataTable dt, DataTable dt2)
        {
            var obj = new object[dt.Columns.Count];
            foreach (DataRow dr in dt2.Rows)
            {
                dr.ItemArray.CopyTo(obj, 0);
                dt.Rows.Add(obj);
            }
            return dt;
        }


        /// <summary>
        /// DataTable转成List
        /// </summary>
        /// <typeparam name="T">List对象类型</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>转换后的List</returns>
        public static List<T> ToList<T>(this DataTable table)
        {
            var list = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                var t = Activator.CreateInstance<T>();
                var propertypes = t.GetType().GetProperties();
                foreach (var pro in propertypes)
                {
                    var tempName = pro.Name;
                    if (!table.Columns.Contains(tempName)) continue;
                    var value = row[tempName];
                    if (value is DBNull) value = null;
                    pro.SetValue(t, value, null);
                }
                list.Add(t);
            }
            return list;
        }

    }
}
