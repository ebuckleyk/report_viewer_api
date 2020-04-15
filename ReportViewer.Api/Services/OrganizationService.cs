using ReportViewer.Domain.Abstractions;
using ReportViewer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportViewer.Api.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _repository;
        public OrganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
        }
        public Task<Organization> AddNewOrganization(Organization organization)
        {
            // TODO: add validation
            return _repository.CreateOrganizationAsync(organization);
        }

        public Task<Organization> GetOrganization(string ownerSub)
        {
            return _repository.GetOrganizationAsync(ownerSub);
        }
    }
}
