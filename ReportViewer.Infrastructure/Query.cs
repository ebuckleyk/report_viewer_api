using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ReportViewer.Infrastructure
{
    public static class Query
    {
        public static string GET_DB_OBJECT_METADATA = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @objectName";
        public static string GET_REPORT_DATA(string columns, string objectName) => $"SELECT {columns} FROM {SimpleSanitize(objectName)}";
        public static string GET_REPORT_DATA_COUNT(string objectName) => $"SELECT COUNT(*) FROM {SimpleSanitize(objectName)}";
        public static string PAGINATION(int pageSize, int pageNumber) => $"ORDER BY 1 OFFSET {pageSize * (pageNumber - 1)} ROWS FETCH NEXT {pageSize} ROWS ONLY;";
        public static string GET_REPORT => "SELECT name, description, view_name FROM dbo.reports WHERE view_name = @view_name";
        private static string SimpleSanitize(string viewName) => Regex.Replace(viewName, @"\s+", string.Empty).Trim();
    }
}
