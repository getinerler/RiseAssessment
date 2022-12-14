using Microsoft.AspNetCore.Mvc;
using ReportMicroservice.Dtos;
using ReportMicroservice.Service;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        public async Task<IActionResult> RequestReport(string country, string city)
        {
            try
            {
                Guid guid = await _service.RequestReport(country, city);
                return Ok(guid);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/reports")]
        public async Task<IActionResult> GetReports()
        {
            try
            {
                List<ReportForListDto> reportList = await _service.GetReports();
                return Ok(reportList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
