using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ReportViewer.Infrastructure
{
    public class DataAccess : IDataAccess
    {
        private readonly string connStr;
        public DataAccess(IConfiguration configuration)
        {
            connStr = configuration["AWS_RDS:ReportDb"];
        }
        public async Task<T> ExecuteCommandAsync<T>(Func<SqlConnection, T> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                return task(conn);
            }
        }

        public async Task ExecuteCommandAsync(Action<SqlConnection> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                task(conn);
            }
        }
    }
}
