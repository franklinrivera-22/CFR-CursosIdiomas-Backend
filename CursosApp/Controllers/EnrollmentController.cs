using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CursosApp.Constants;
using CursosApp.Services.Enrollments;

namespace CursosApp.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentsService _enrollmentsService;

        public EnrollmentController(IEnrollmentsService enrollmentsService)
        {
            _enrollmentsService = enrollmentsService;
        }

        [HttpGet("my-courses")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RolesConstant.NORMAL_USER)]
        public async Task<ActionResult> GetMyCourses()
        {
            var userId = User.FindFirstValue("UserId");
            var response = await _enrollmentsService.GetMyCoursesAsync(userId);
            return StatusCode(response.StatusCode, response);
        }
    }
}