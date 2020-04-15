using Dapper;
using ReportViewer.Domain.Abstractions;
using ReportViewer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportViewer.Infrastructure
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDataAccess _dataAccess;
        public ReportRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> ReportDataCountAsync(string viewName)
        {
            int count = 0;
            await _dataAccess.ExecuteCommandAsync(x =>
            {
                count = x.ExecuteScalar<int>(Query.GET_REPORT_DATA_COUNT(viewName));
            });
            return count;
        }

        public async Task<Report> GetReportDataAsync(string viewName, int pageSize, int pageNumber)
        {

            var report = await GetReportAsync(viewName);
            var metadata = await GetReportMetaDataAsync(viewName);
            report.AddMetaData(metadata);
            var columns = metadata.Select(x => x.ColumnName);
            var formatted_columns = string.Join(", ", columns);
            var query = new StringBuilder();
            query.AppendLine(Query.GET_REPORT_DATA(formatted_columns, viewName));
            query.AppendLine(Query.PAGINATION(pageSize, pageNumber));

            await _dataAccess.ExecuteCommandAsync(x =>
            {
                var reader = x.ExecuteReader(query.ToString());

                while (reader.Read())
                {
                    var data = new Dictionary<string, object>();
                    foreach (string c in columns)
                    {
                        var value = reader[c];
                        data.Add(c, value);
                    }
                    report.AddData(data);
                }
            });
            return report;
        }

        private async Task<Report> GetReportAsync(string viewName)
        {
            Report ret = null;

            await _dataAccess.ExecuteCommandAsync(x =>
            {
                var reader = x.ExecuteReader(Query.GET_REPORT, new { view_name = viewName });
                while (reader.Read())
                {
                    ret = new Report(reader["name"] as string, reader["description"] as string, viewName);
                }
            });

            return ret;
        }

        public async Task<IReadOnlyCollection<Report>> GetReportListAsync(string sub)
        {
            var ret = new List<Report>();

            await _dataAccess.ExecuteCommandAsync(x =>
            {
                var reader = x.ExecuteReader(GET_REPORT_LIST, new { SubjectId = sub });
                while (reader.Read())
                {
                    ret.Add(new Report(reader["name"] as string, reader["description"] as string, reader["view_name"] as string));
                }
            });

            return ret.AsReadOnly();
        }

        private async Task<List<ReportMetaData>> GetReportMetaDataAsync(string viewName)
        {
            var ret = new List<ReportMetaData>();

            await _dataAccess.ExecuteCommandAsync(x =>
            {
                var reader = x.ExecuteReader(Query.GET_DB_OBJECT_METADATA, new { objectName = viewName});

                while (reader.Read())
                {
                    var metaData = new ReportMetaData(reader["COLUMN_NAME"] as string, reader["DATA_TYPE"] as string);
                    ret.Add(metaData);
                }
            });

            return ret;
        }

        private static string GET_REPORT_LIST = @"SELECT r.name, r.description, r.view_name FROM [dbo].[reports] r
INNER JOIN dbo.organizations o ON o.Id = r.OrganizationId
WHERE o.OwnerSub = @SubjectId";
    }
}
