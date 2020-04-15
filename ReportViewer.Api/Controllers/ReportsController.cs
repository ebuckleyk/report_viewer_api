using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportViewer.Domain.Abstractions;

namespace ReportViewer.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportsController : ApiBaseController
    {
        private readonly IReportService _service;
        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            return Ok(await _service.GetReports(UserContext));
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetReport(string key, [FromQuery]int? pageNumber = null, [FromQuery]int? pageSize = null)
        {
            return Ok(await _service.GetReport(key, pageNumber, pageSize));
        }
    }
}