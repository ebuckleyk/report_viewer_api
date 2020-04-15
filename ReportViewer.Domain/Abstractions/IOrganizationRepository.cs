using ReportViewer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReportViewer.Domain.Abstractions
{
    public interface IOrganizationRepository
    {
        Task<IReadOnlyCollection<Organization>> GetOrganizationsAsync();
        Task<Organization> GetOrganizationAsync(string ownerSub);
        Task<Organization> CreateOrganizationAsync(Organization organization);
    }
}
