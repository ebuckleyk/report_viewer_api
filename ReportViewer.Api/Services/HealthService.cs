using Dapper;
using Microsoft.Extensions.Logging;
using ReportViewer.Domain.Abstractions;
using ReportViewer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportViewer.Api.Services
{
    public class HealthService : IHealthService
    {
        private readonly ILogger<HealthService> _logger;
        private readonly IDataAccess _dataAccess;
        public HealthService(ILogger<HealthService> logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }

        public async Task CheckDbConnection()
        {
            using (_logger.BeginScope(" ----- Checking Db Connection -----"))
            {
                await _dataAccess.ExecuteCommandAsync(x =>
                {
                    x.Execute("SELECT TOP 1 * FROM dbo.reports");
                });
            }
        }
    }
}
