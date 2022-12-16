﻿using Microsoft.AspNetCore.Mvc;
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
                if (info.Status == ReportStatus.Completed.ToString())
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

        [HttpGet("reports")]
        public async Task<IActionResult> GetReports()
        {
            try
            {
                string location = $"{Request.Scheme}://{Request.Host}{Request.Path}";
                List<ReportForListDto> reportList = await _service.GetReports();
                foreach (ReportForListDto item in reportList)
                {
                    if (item.Status == ReportStatus.Completed.ToString())
                    {
                        item.Path = location + item.Guid + ".xlsx";
                    }
                }
                return Ok(reportList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
