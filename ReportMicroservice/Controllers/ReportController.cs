using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ReportMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        public ReportController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> Info(Guid guid)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
