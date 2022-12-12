using ContactMicroservice.Controllers;
using ContactMicroservice.Dtos;
using ContactMicroservice.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ContactMicroserviceUnitTest
{
    public class UnitTests
    {
        private readonly PhoneBookController _controller;
        private readonly IPhoneBookService _service;

        public UnitTests()
        {
            _service = new PhoneBookServiceForUnitTest();
            _controller = new PhoneBookController(_service);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            Guid existingGuid = new Guid("ec2d9314-34e1-4f4e-9d03-a877920dfb5a");
            IActionResult okResult = await _controller.Get(existingGuid);

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            OkObjectResult okResult = await _controller.GetList() as OkObjectResult;
            var items = Assert.IsType<List<PhoneBookItemForListDto>>(okResult.Value);

            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            Guid testGuid = new Guid("ec2d9314-34e1-4f4e-9d03-a877920dfb5a");
            var okResult = await _controller.Get(testGuid);

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async Task GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            Guid testGuid = new Guid("ec2d9314-34e1-4f4e-9d03-a877920dfb5a");
            OkObjectResult okResult = await _controller.Get(testGuid) as OkObjectResult;

            Assert.IsType<PhoneBookItemDetailDto>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as PhoneBookItemDetailDto).Guid);
        }

        [Fact]
        public async Task Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            PhoneBookItemAddDto nameMissingItem = new PhoneBookItemAddDto()
            {
                Name = "Elvis",
                Surname = "Presley",
                Firm = "No Firm",
                Phone = "+9900",
                Mail = "model@model.com",
                Country = "USA",
                City = "Minessota"
            };
            _controller.ModelState.AddModelError("Name", "Required");
            IActionResult badResponse = await _controller.Save(nameMissingItem);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async Task Add_ValidObjectPassed_ReturnsOkResponse()
        {
            PhoneBookItemAddDto testItem = new PhoneBookItemAddDto()
            {
                Name = "Elvis",
                Surname = "Presley",
                Firm = "No Firm",
                Phone = "+9900",
                Mail = "model@model.com",
                Country = "USA",
                City = "Minessota"
            };
            IActionResult okResponse = await _controller.Save(testItem);

            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public async Task Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            Guid notExistingGuid = Guid.NewGuid();
            IActionResult badResponse = await _controller.Delete(notExistingGuid);

            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public async Task Remove_ExistingGuidPassed_ReturnsNoContentResult()
        {
            Guid existingGuid = new Guid("06a25f72-3e79-49ce-a95f-ab982ee43ded");
            IActionResult noContentResponse = await _controller.Delete(existingGuid);

            Assert.IsType<NoContentResult>(noContentResponse);
        }

        [Fact]
        public async Task Remove_ExistingGuidPassed_RemovesOneItem()
        {
            Guid existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            await _controller.Delete(existingGuid);
            List<PhoneBookItemForListDto> list = await _service.GetList();

            //Eklemeler sırasında 2 tane sildik. İki başarılı silme denemesi yaptık. 
            //Toplam rakam gene başa dönmüş olmalı.
            Assert.Equal(3, list.Count());
        }
    }
}
