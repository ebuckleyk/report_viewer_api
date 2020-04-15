using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportViewer.Api.Models;
using ReportViewer.Domain.Abstractions;
using ReportViewer.Domain.Models;

namespace ReportViewer.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrganizationsController : ApiBaseController
    {
        private readonly IOrganizationService _service;
        public OrganizationsController(IOrganizationService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest createReq)
        {
            return Ok(await _service.AddNewOrganization(new Organization(createReq.Name, createReq.OwnerSub, createReq.Address1, createReq.Address2, createReq.Phone)));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetOrganization(UserContext.Sub));
        }
    }
}