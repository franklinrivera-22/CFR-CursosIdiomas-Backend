
using CursosApp.Dtos.Common;
using CursosApp.Dtos.Enrollments;

namespace CursosApp.Services.Enrollments
{
    public interface IEnrollmentsService
    {
  
        Task<ResponseDto<List<EnrollmentDto>>> GetMyCoursesAsync(string userId);
    
    }
}