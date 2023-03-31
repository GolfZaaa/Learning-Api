using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.DAL.Queries;
using TestApi.DAL.Response;
using TestApi.Models;

namespace TestApi.Controllers
{
 [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {

        private readonly ProjectQ _projectQ = new ProjectQ();

        [HttpGet("GetProject")]
        public async Task<IActionResult> GetProfile(int pageSize = 10, int currentPage = 1, string search = "")
        {
            try
            {
                var response = await _projectQ.GetProject(pageSize, currentPage, search);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorMessages { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("DeleteProject/{code}")]

        public async Task<IActionResult> DeleteProject(string code)
        {
            try
            {
                var response = await _projectQ.DeleteProject(code);
                return StatusCode(response.StatusCode, response);
            }catch(Exception e)
            {
                return BadRequest(new ResponseErrorMessages {StatusCode = 400, Message = e.Message});
            }

        }

        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateCourse([FromForm] Project projectDT)
        {
            try
            {
                var response = await _projectQ.CreateProject(projectDT);
                return StatusCode(200, response);
            }
            catch(Exception e)
            {
                return BadRequest(new ResponseErrorMessages { StatusCode = 400,Message = e.Message});
            }
        }

        [HttpPut("EditProject/{code}")]
        public async Task<IActionResult> EditCourse(string code, [FromForm] Project projectDT)
        {
            try
            {
                var response = await _projectQ.EditProject(code, projectDT);
                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorMessages {StatusCode = 400,Message =e.Message});
            }
        }

    }
}
