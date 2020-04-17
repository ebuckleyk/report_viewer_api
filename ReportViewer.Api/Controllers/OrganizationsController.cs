using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OrganizationsController> _logger;
        public OrganizationsController(ILogger<OrganizationsController> logger, IOrganizationService service)
        {
            _service = service;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest createReq)
        {
            using (_logger.BeginScope("----- Create -----"))
            {
                try
                {
                    return Ok(await _service.AddNewOrganization(new Organization(createReq.Name, createReq.OwnerSub, createReq.Address1, createReq.Address2, createReq.Phone)));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);
                }
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (_logger.BeginScope("----- Get -----"))
            {
                try
                {
                    return Ok(await _service.GetOrganization(UserContext.Sub));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}