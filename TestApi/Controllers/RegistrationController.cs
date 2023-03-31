using Microsoft.AspNetCore.Mvc;
using TestApi.DAL.Queries;
using TestApi.DAL.Response;
using TestApi.Models;

namespace TestApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {

        private readonly RegistrationQ _registration = new RegistrationQ();


        [HttpGet("GetRegistration")]
        public async Task<IActionResult> GetRegistration(int pageSize = 10, int currentPage = 1, string search = "")
        {
            try
            {
                var response = await _registration.GetRegistration(pageSize, currentPage, search);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorMessages { StatusCode = 400, Message = e.Message });
            }
        }


        [HttpDelete("DeleteRegistration/{code}")]

        public async Task<IActionResult> DeleteRegistration(int Id)
        {
            try
            {
                var response = await _registration.DeleteRegistration(Id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorMessages { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPut("EditRegistration/{code}")]
        public async Task<IActionResult> EditReg(int Id, [FromForm] Registration registration)
        {
            try
            {
                var response = await _registration.EditRegistration(Id, registration);
                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorMessages { StatusCode = 400, Message = e.Message });
            }
        }


        [HttpPost("CreateRegistration")]
        public async Task<IActionResult> CreateRegistration([FromForm] Registration registration)
        {
            try
            {
                var response = await _registration.CreateRegistration(registration);
                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorMessages { StatusCode = 400, Message = e.Message });
            }
        }


    }
}
