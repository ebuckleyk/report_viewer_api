using ReportViewer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReportViewer.Domain.Abstractions
{
    public interface IOrganizationService
    {
        Task<Organization> AddNewOrganization(Organization organization);
        Task<Organization> GetOrganization(string ownerSub);
    }
}
