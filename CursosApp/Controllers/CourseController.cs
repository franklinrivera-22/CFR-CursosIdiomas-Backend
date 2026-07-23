using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CursosApp.Constants;
using CursosApp.Dtos.Courses;
using CursosApp.Services.Courses;

namespace CursosApp.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetPage(
            string searchTerm = "", string categoryId = "", int page = 1, int pageSize = 10)
        {
            var response = await _courseService.GetPageAsync(searchTerm, categoryId, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetOne(string id)
        {
            var response = await _courseService.GetOneByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RolesConstant.ADMIN)]
        public async Task<ActionResult> Create([FromBody] CourseCreateDto dto)
        {
            var response = await _courseService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RolesConstant.ADMIN)]
        public async Task<ActionResult> Update(string id, [FromBody] CourseEditDto dto)
        {
            var response = await _courseService.EditAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RolesConstant.ADMIN)]
        public async Task<ActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}