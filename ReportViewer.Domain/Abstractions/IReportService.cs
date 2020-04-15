using ReportViewer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReportViewer.Domain.Abstractions
{
    public interface IReportService
    {
        Task<IReadOnlyCollection<Report>> GetReports(IUserContext userContext);
        Task<PagedResult<Report, IReadOnlyCollection<IDictionary<string, object>>>> GetReport(string key, int? pageNumber, int? pageSize);
    }
}
