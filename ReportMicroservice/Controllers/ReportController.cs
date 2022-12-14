using Microsoft.AspNetCore.Mvc;
using ReportMicroservice.Dtos;
using ReportMicroservice.Service;
using System;
using System.Threading.Tasks;

namespace ReportMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Info(Guid guid)
        {
            try
            {
                ReportInfo info = await _service.GetReportInfo(guid);
                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
