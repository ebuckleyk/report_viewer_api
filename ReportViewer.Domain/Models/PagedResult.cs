using System;
using System.Collections.Generic;
using System.Text;

namespace ReportViewer.Domain.Models
{
    public class PagedResult<TReport, TReportData>
    {
        public PagedResult(int total, int pageSize, int pageNumber, TReport report, TReportData reportData)
        {
            Total = total;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Report = report;
            Data = reportData;
        }
        public int Total { get; }
        public int PageSize { get; }
        public int PageNumber { get; }
        public TReport Report { get; }
        public TReportData Data { get; }
    }
}
