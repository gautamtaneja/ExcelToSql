using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelToSql.Services
{
    public class ExcelServices
    {
        public static List<T> GetList<T>(ExcelWorksheet sheet)
        {
            List<T> list = new List<T>();
            //first row is for knowing the properties of object
            var columnInfo = Enumerable.Range(1, sheet.Dimension.Columns).ToList().Select(n =>

                new { Index = n, ColumnName = sheet.Cells[1, n].Value.ToString() }
            );

            for (int row = 2; row <= sheet.Dimension.Rows; row++)
            {
                T obj = (T)Activator.CreateInstance(typeof(T));//generic object
                foreach (var prop in typeof(T).GetProperties())
                {
                    int col = columnInfo.SingleOrDefault(c => c.ColumnName == prop.Name).Index;
                    var val = sheet.Cells[row, col].Value;
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(val, propType));
                }
                list.Add(obj);
            }

            return list;
        }
    }
}
