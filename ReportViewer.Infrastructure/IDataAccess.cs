using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ReportViewer.Infrastructure
{
    public interface IDataAccess
    {
        Task<T> ExecuteCommandAsync<T>(Func<SqlConnection, T> task);
        Task ExecuteCommandAsync(Action<SqlConnection> task);
    }
}
