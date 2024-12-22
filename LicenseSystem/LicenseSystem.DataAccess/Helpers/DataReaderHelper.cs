using System;
using System.Data.SqlClient;


namespace LicenseSystem.DataAccess.Connection
{
    // LicenseSystem.DataAccess/Helpers/DataReaderHelper.cs
    namespace LicenseSystem.DataAccess.Helpers
    {
        public static class DataReaderHelper
        {
            public static T GetValueOrDefault<T>(this SqlDataReader reader, string columnName, T defaultValue = default)
            {
                var ordinal = reader.GetOrdinal(columnName);
                if (reader.IsDBNull(ordinal))
                    return defaultValue;

                return (T)reader.GetValue(ordinal);
            }

            public static DateTime? GetNullableDateTime(this SqlDataReader reader, string columnName)
            {
                var ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? (DateTime?)null : reader.GetDateTime(ordinal);

            }

            public static int? GetNullableInt32(this SqlDataReader reader, string columnName)
            {
                var ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? (int?)null : reader.GetInt32(ordinal); 
            }

            public static string GetNullableString(this SqlDataReader reader, string columnName)
            {
                var ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
            }
        }
    }
}
