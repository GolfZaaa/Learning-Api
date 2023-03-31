using Microsoft.AspNetCore.Mvc;
using TestApi.DAL.Queries;
using TestApi.DAL.Response;
using TestApi.Models;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificateController : Controller
    {
        private readonly CertificateQ _certificateQ = new CertificateQ();


        [HttpGet("GetCertificate")]
        public async Task<IActionResult> GetProfileCertificate(int pageSize = 10, int currentPage = 1, string search = "")
        {
            try
            {
                var response = await _certificateQ.GetCertificate(pageSize, currentPage, search);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorMessages { StatusCode = 400, Message = e.Message });
            }
        }


        [HttpGet("GetCertificateByID")]
        public async Task<IActionResult> GetProfileCertificateById(int id)
        {
            try
            {
                var response = await _certificateQ.GetPrintCertificate(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorMessages { StatusCode = 400, Message = e.Message });
            }
        }


        [HttpPost("CreateCertificate")]
        public async Task<IActionResult> CreateCertificate([FromForm] Certificate certDT, string projectCode)
        {
            try
            {
                var response = await _certificateQ.CreateCertificate(certDT,projectCode);
                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorMessages { StatusCode = 400, Message = e.Message });
            }
        }





    }
}
