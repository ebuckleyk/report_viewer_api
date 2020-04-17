using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ReportViewer.Infrastructure
{
    public class DataAccess : IDataAccess
    {
        private readonly string connStr;
        private readonly ILogger<DataAccess> _logger;
        public DataAccess(ILogger<DataAccess> logger, IConfiguration configuration)
        {
            connStr = configuration["AWS_RDS:ReportDb"];
            _logger = logger;
        }
        public async Task<T> ExecuteCommandAsync<T>(Func<SqlConnection, T> task)
        {
            using (_logger.BeginScope("----- ExecuteCommandAsync<T> -----"))
            using (var conn = new SqlConnection(connStr))
            {
                try
                {
                    _logger.LogInformation("Opening connection");
                    await conn.OpenAsync();
                    return task(conn);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw ex;
                }
            }
        }

        public async Task ExecuteCommandAsync(Action<SqlConnection> task)
        {
            using (_logger.BeginScope("----- ExecuteCommandAsync -----"))
            using (var conn = new SqlConnection(connStr))
            {
                try
                {
                    _logger.LogInformation("Opening connection");
                    await conn.OpenAsync();
                    task(conn);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw ex;
                }
            }
        }
    }
}
