using CursosApp.Dtos.Common;
using CursosApp.Dtos.Courses;
namespace CursosApp.Services.Courses
{
    public interface ICourseService
    {
        Task<ResponseDto<PageDto<List<CourseDto>>>> GetPageAsync(
            string searchTerm = "", string categoryId = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<CourseDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<CourseActionResponseDto>> CreateAsync(CourseCreateDto dto);
        Task<ResponseDto<CourseActionResponseDto>> EditAsync(string id, CourseEditDto dto);
        Task<ResponseDto<CourseActionResponseDto>> DeleteAsync(string id);
    }
}