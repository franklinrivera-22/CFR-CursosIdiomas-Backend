using CursosApp.Constants;
using CursosApp.Database;
using CursosApp.Dtos.Common;
using CursosApp.Mappers;
using CursosApp.Services.Enrollments;
using Microsoft.EntityFrameworkCore;


namespace CursosApp.Dtos.Enrollments
{

    public class EnrollmentService : IEnrollmentsService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<EnrollmentDto>>> GetMyCoursesAsync(string userId)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Course)
                    .ThenInclude(c => c.Category)
                .Where(e => e.UserId == userId && e.IsActive)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();

            return new ResponseDto<List<EnrollmentDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Cursos del usuario obtenidos correctamente.",
                Data = EnrollmentMapper.ListEntityToListDto(enrollments)
            };
        }

    }
}