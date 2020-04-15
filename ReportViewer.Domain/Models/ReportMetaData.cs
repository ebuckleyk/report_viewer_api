using System;
using System.Collections.Generic;
using System.Text;

namespace ReportViewer.Domain.Models
{
    public class ReportMetaData
    {
        public ReportMetaData(string columnName, string columnType)
        {
            ColumnName = columnName;
            ColumnType = columnType;
        }

        /// <summary>
        /// Column name
        /// </summary>
        public string ColumnName { get; }
        /// <summary>
        /// Column type
        /// </summary>
        public string ColumnType { get; }
    }
}
