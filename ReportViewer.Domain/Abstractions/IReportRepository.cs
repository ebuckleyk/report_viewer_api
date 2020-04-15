using ReportViewer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReportViewer.Domain.Abstractions
{
    public interface IReportRepository
    {
        /// <summary>
        /// Get row count of report data
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        Task<int> ReportDataCountAsync(string viewName);
        /// <summary>
        /// Get list of available reports
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyCollection<Report>> GetReportListAsync(string sub);
        /// <summary>
        /// Get paginated report data for specified report
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<Report> GetReportDataAsync(string viewName, int pageSize, int pageNumber);
    }
}
