﻿using ReportMicroservice.Dtos;
using ReportMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportMicroservice.Service
{
    public class ReportServiceForUnitTest : IReportService
    {
        private readonly List<Report> reports;

        public ReportServiceForUnitTest()
        {
            reports = new List<Report>()
            {
                new Report()
                {
                    ReportId = 1,
                    CreatedDate = new DateTime(2022, 12, 01),
                    ExcelFileReady = false,
                    Guid = new Guid("ec2d9314-34e1-4f4e-9d03-a877920dfb5a")
                },
                 new Report()
                {
                    ReportId = 2,
                    CreatedDate = new DateTime(2022, 12, 02),
                    ExcelFileReady = false,
                    Guid = new Guid("9d56cba7-9da9-4ea2-a3bf-68bcb161a9b5")
                },
                new Report()
                {
                    ReportId = 3,
                    CreatedDate = new DateTime(2022, 12, 03),
                    ExcelFileReady = true,
                    Guid = new Guid("b835ebc6-e521-4a73-9146-e4410cdd55a5")
                }
            };
        }

        public async Task<ReportInfo> GetReportInfo(Guid guid)
        {
            Report report = reports.FirstOrDefault();
            if (report == null)
            {
                return new ReportInfo()
                {
                    Status = ReportStatus.NotQueued.ToString()
                };
            }

            ReportInfo info = new ReportInfo()
            {
                Status = !report.ExcelFileReady ? 
                    ReportStatus.Processing.ToString() : 
                    ReportStatus.Completed.ToString(),
                Path = report.ExcelFileReady ? "/ExcelFiles/" + report.Guid + ".xlsx" : ""
            };

            return info;
        }

        public async Task<Guid> RequestReport()
        {
            Report newReport = new Report()
            {
                ReportId = reports.Max(x => x.ReportId) + 1,
                CreatedDate = new DateTime(2022, 12, 03),
                ExcelFileReady = true,
                Guid = Guid.NewGuid()
            };

            RabbitMQHelper.RabbitMQReceiveHelper.SendNewRequest(newReport.Guid);
            return newReport.Guid;
        }

        public async Task<List<ReportForListDto>> GetReports()
        {
            return reports
                .Select(x => new ReportForListDto() 
                {
                    CreatedDate = x.CreatedDate,
                    Guid = x.Guid
                })
                .ToList();
        }
    }
}
