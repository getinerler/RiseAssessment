using ContactMicroservice.Dtos;
using ContactMicroservice.Exceptions;
using ContactMicroservice.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace ContactMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookService _service;

        public PhoneBookController(IPhoneBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                return Ok(await _service.GetList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPhoneBookItem")]
        public async Task<IActionResult> Get(Guid guid)
        {
            try
            {
                return Ok(await _service.Get(guid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Save(PhoneBookItemAddDto item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(ModelError(ModelState));
                }
                Guid guid = await _service.Save(item);
                return Ok(guid);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid guid)
        {
            try
            {
                await _service.Delete(guid);
                return new NoContentResult();
            }
            catch (ItemNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid guid, string key, string value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }
                await _service.Update(guid, key, value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string ModelError(ModelStateDictionary state)
        {
            foreach (var i in state.Values)
            {
                if (i.Errors != null)
                {
                    foreach (var j in i.Errors)
                    {
                        string errors = "";

                        if (j.ErrorMessage != null)
                            errors += j.ErrorMessage;

                        if (j.Exception != null && j.Exception.Message != null)
                            errors += "    " + j.Exception.Message;

                        return errors;
                    }
                }
            }
            return "";
        }
    }
}
