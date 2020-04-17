using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportViewer.Domain.Abstractions;

namespace ReportViewer.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportsController : ApiBaseController
    {
        private readonly IReportService _service;
        private readonly ILogger<ReportsController> _logger;
        public ReportsController(ILogger<ReportsController> logger, IReportService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            using (_logger.BeginScope("----- GetReports -----"))
            {
                try
                {
                    return Ok(await _service.GetReports(UserContext));
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetReport(string key, [FromQuery]int? pageNumber = null, [FromQuery]int? pageSize = null)
        {
            using (_logger.BeginScope("----- GetReport -----"))
            {
                try
                {
                    return Ok(await _service.GetReport(key, pageNumber, pageSize));
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}