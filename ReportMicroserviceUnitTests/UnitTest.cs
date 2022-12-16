using Microsoft.AspNetCore.Mvc;
using ReportMicroservice.Controllers;
using ReportMicroservice.Dtos;
using ReportMicroservice.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ReportMicroserviceUnitTests
{
    public class UnitTest
    {
        private readonly ReportController _controller;
        private readonly ReportServiceForUnitTest _service;

        public UnitTest()
        {
            _service = new ReportServiceForUnitTest();
            _controller = new ReportController(_service);
        }

        //Info method tests
        [Fact]
        public async Task Info_WhenCalled_ReturnsOkResult()
        {
            Guid existingGuid = new Guid("ec2d9314-34e1-4f4e-9d03-a877920dfb5a");
            IActionResult okResult = await _controller.Info(existingGuid);

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async Task Info_ExistingGuidPassed_ReturnsRightItem()
        {
            Guid testGuid = new Guid("ec2d9314-34e1-4f4e-9d03-a877920dfb5a");
            OkObjectResult okResult = await _controller.Info(testGuid) as OkObjectResult;

            string excelPath = string.Empty;

            Assert.IsType<ReportInfo>(okResult.Value);
            Assert.Equal((okResult.Value as ReportInfo).Status, ReportStatus.Processing.ToString());
            Assert.Equal((okResult.Value as ReportInfo).Path, excelPath);
        }

        //GetReports
        [Fact]
        public async Task GetReports_WhenCalled_ReturnsOkResult()
        {
            OkObjectResult okResult = await _controller.GetReports() as OkObjectResult;
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetReports_WhenCalled_ReturnsAllItems()
        {
            OkObjectResult okResult = await _controller.GetReports() as OkObjectResult;
            var items = Assert.IsType<List<ReportForListDto>>(okResult.Value);

            Assert.Equal(3, items.Count);
        }

        //Request report tests
        [Fact]
        public async Task RequestReport_WhenCalled_ReturnsOkResult()
        {
            IActionResult okResult = await _controller.RequestReport();
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async Task RequestReport_ExistingGuidPassed_ReturnsRightItem()
        {
            IActionResult okResult = await _controller.RequestReport();
            OkObjectResult objResult = okResult as OkObjectResult;
            Assert.IsType<Guid>(objResult.Value);
        }
    }
}
