using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportViewer.Domain.Abstractions;
using ReportViewer.Domain.Models;

namespace ReportViewer.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        protected IUserContext UserContext => new UserContext(User);
    }
}