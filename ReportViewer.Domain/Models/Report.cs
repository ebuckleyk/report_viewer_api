using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportViewer.Domain.Models
{
    public class Report
    {
        public Report(string name, string desc, string key)
        {
            Name = name;
            Description = desc;
            Key = key;
        }

        /// <summary>
        /// Report Name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Report Description
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// View name to be invoked to get report data
        /// </summary>
        public string Key { get; private set; }

        private List<IDictionary<string, object>> _data 
            = new List<IDictionary<string, object>>();

        /// <summary>
        /// Dynamic data keyed by column name with value
        /// </summary>
        public IReadOnlyCollection<IDictionary<string, object>> Data 
            => _data.AsReadOnly();

        private List<ReportMetaData> _reportMetaData 
            = new List<ReportMetaData>();
        /// <summary>
        /// Schema metadata information for the report
        /// </summary>
        public IReadOnlyCollection<ReportMetaData> ReportMetaData 
            => _reportMetaData.AsReadOnly();
        /// <summary>
        /// Add metadata to report
        /// </summary>
        /// <param name="reportMetaData"></param>
        public void AddMetaData(IEnumerable<ReportMetaData> reportMetaData)
        {
            _reportMetaData.AddRange(reportMetaData);
        }
        /// <summary>
        /// Add report data
        /// </summary>
        /// <param name="data"></param>
        public void AddData(IDictionary<string, object> data)
        {
            _data.Add(data);
        }

        public void ProtectViewName(IDataProtector protector)
        {
            if (string.IsNullOrEmpty(Key)) throw new Exception("Can't protect view name. It is null or empty");
            Key = protector.Protect(Key);
        }
    }
}
