using Microsoft.Extensions.Configuration;
using ReportViewer.Domain.Abstractions;
using ReportViewer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ReportViewer.Infrastructure
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IDataAccess _dataAccess;
        public OrganizationRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IReadOnlyCollection<Organization>> GetOrganizationsAsync()
        {
            var ret = await _dataAccess.ExecuteCommandAsync(x =>
            {
                return x.Query<Organization>(GET_ALL);
            });

            return ret.AsList().AsReadOnly();
        }

        public async Task<Organization> GetOrganizationAsync(string ownerSub)
        {
            var ret = await _dataAccess.ExecuteCommandAsync(x =>
            {
                return x.QuerySingleOrDefault<Organization>(GET_ALL_BY_OWNER, new { OwnerSub = ownerSub });
            });

            return ret;
        }

        public async Task<Organization> CreateOrganizationAsync(Organization organization)
        {
            Organization ret = null;
            await _dataAccess.ExecuteCommandAsync(x =>
            {
                var id = x.ExecuteScalar<int>(
                    INSERT_ORGANIZATION, 
                    new { 
                        Name = organization.Name, 
                        Address1 = organization.Address1, 
                        Address2 = organization.Address2 ,
                        Phone = organization.Phone,
                        OwnerSub = organization.OwnerSub
                    });

                ret = new Organization(id, organization.OwnerSub, organization.Name, organization.Address1, organization.Address2, organization.Phone);
            });
            return ret;
        }

        private static string GET_ALL = "SELECT [Id], [Name], [Address1], [Address2], [Phone], [OwnerSub] FROM dbo.organizations";
        private static string GET_ALL_BY_OWNER = $"{GET_ALL} WHERE OwnerSub = @OwnerSub";
        private static string INSERT_ORGANIZATION = "INSERT INTO dbo.organizations ([Name], [Address1], [Address2], [Phone], [OwnerSub]) VALUES (@Name, @Address1, @Address2, @Phone, @OwnerSub); SELECT SCOPE_IDENTITY();";
    }
}
