using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSystem.Common.Exceptions
{
    // Extensions/DataRowExtensions.cs
    public static class DataRowExtensions
    {
        public static T GetValueOrDefault<T>(this DataRow row, string columnName, T defaultValue = default)
        {
            if (row == null || !row.Table.Columns.Contains(columnName) || row.IsNull(columnName))
                return defaultValue;

            try
            {
                return (T)Convert.ChangeType(row[columnName], typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetString(this DataRow row, string columnName)
            => GetValueOrDefault<string>(row, columnName, string.Empty);

        public static int GetInt32(this DataRow row, string columnName)
            => GetValueOrDefault<int>(row, columnName);

        public static bool GetBoolean(this DataRow row, string columnName)
            => GetValueOrDefault<bool>(row, columnName);

        public static DateTime GetDateTime(this DataRow row, string columnName)
            => GetValueOrDefault<DateTime>(row, columnName);

        public static decimal GetDecimal(this DataRow row, string columnName)
            => GetValueOrDefault<decimal>(row, columnName);
    }
}
