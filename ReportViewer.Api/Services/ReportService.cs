using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using ReportViewer.Domain.Abstractions;
using ReportViewer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportViewer.Api.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;
        private readonly IDataProtector _protector;
        public ReportService(IReportRepository repository, IDataProtectionProvider provider, IConfiguration configuration)
        {
            _repository = repository;
            _protector = provider.CreateProtector(configuration["EncryptionKey"]);
        }

        public async Task<IReadOnlyCollection<Report>> GetReports(IUserContext userContext)
        {
            var ret = await _repository.GetReportListAsync(userContext.Sub);

            foreach(var r in ret)
            {
                r.ProtectViewName(_protector);
            }

            return ret;
        }

        public async Task<PagedResult<Report, IReadOnlyCollection<IDictionary<string, object>>>> GetReport(string key, int? pageNumber, int? pageSize)
        {
            var pNumber = pageNumber ?? 1;
            var unprotectedKey = _protector.Unprotect(key);
            var reportDataTotal = await _repository.ReportDataCountAsync(unprotectedKey);
            var pgSize = pageSize ?? reportDataTotal;
            var report = await _repository.GetReportDataAsync(unprotectedKey, pgSize, pNumber);
            report.ProtectViewName(_protector);
            var ret = new PagedResult<Report, IReadOnlyCollection<IDictionary<string, object>>>(reportDataTotal, pgSize, pNumber, report, report.Data);
            return ret;
        }
    }
}
