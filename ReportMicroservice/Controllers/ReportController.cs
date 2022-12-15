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
        private readonly IHangfireService _hangfireService;
        private readonly IReportService _service;

        public ReportController(IHangfireService hang, IReportService service)
        {
            _hangfireService = hang;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Info(Guid guid)
        {
            try
            {
                ReportInfo info = await _service.GetReportInfo(guid);

                if (!string.IsNullOrEmpty(info.Path))
                {
                    string location = $"{Request.Scheme}://{Request.Host}{Request.Path}";
                    info.Path = location + info.Path;
                }

                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("bıdı")]
        public async Task<IActionResult> Bıdı()
        {
            try
            {

                List<RabbitMqMessage> requests = await _hangfireService.GetRequests();

                await _hangfireService.CreateExcelFiles(requests);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> RequestReport()
        {
            try
            {
                Guid guid = await _service.RequestReport();
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
