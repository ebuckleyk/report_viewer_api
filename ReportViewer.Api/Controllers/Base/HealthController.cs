using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportViewer.Domain.Abstractions;

namespace ReportViewer.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly IHealthService _service;
        public HealthController(ILogger<HealthController> logger, IHealthService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Get()
        {
            using(_logger.BeginScope("----- Beginning Health Check -----"))
            {
                try
                {
                    await _service.CheckDbConnection();
                    return Ok("Health Check OK!!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}