using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReportViewer.Domain.Abstractions
{
    public interface IHealthService
    {
        Task CheckDbConnection();
    }
}
