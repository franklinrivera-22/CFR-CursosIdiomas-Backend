using Microsoft.EntityFrameworkCore;

using CursosApp.Database;
using CursosApp.Dtos.Common;
using CursosApp.Dtos.Courses;
using CursosApp.Entities;
using CursosApp.Constants;
using CursosApp.Mappers;

namespace CursosApp.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public CourseService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }


        public async Task<ResponseDto<PageDto<List<CourseDto>>>> GetPageAsync(
            string searchTerm = "", string categoryId = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);


            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<CourseEntity> query = _context.Courses.Include(c => c.Category);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x =>
                    (x.Title + " " + x.Description + " " + x.Level).Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            int totalRows = await query.CountAsync();

            var entities = await query
                .OrderBy(x => x.Title)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            return new ResponseDto<PageDto<List<CourseDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<CourseDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = CourseMapper.ListEntityToListDto(entities),
                    HasNextPage = page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<CourseDto>> GetOneByIdAsync(string id)
        {
            var entity = await _context.Courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity is null)
            {
                return new ResponseDto<CourseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            return new ResponseDto<CourseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = CourseMapper.EntityToDto(entity)
            };
        }

        public async Task<ResponseDto<CourseActionResponseDto>> CreateAsync(CourseCreateDto dto)
        {
            // Validamos que la categoría exista antes de crear.
            bool categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
            {
                return new ResponseDto<CourseActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "La categoría indicada no existe."
                };
            }

            var entity = CourseMapper.CreateDtoToEntity(dto);
            _context.Courses.Add(entity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CourseActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Data = new CourseActionResponseDto { Id = entity.Id }
            };
        }

        public async Task<ResponseDto<CourseActionResponseDto>> EditAsync(string id, CourseEditDto dto)
        {
            var entity = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (entity is null)
            {
                return new ResponseDto<CourseActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            var updated = CourseMapper.EditDtoToEntity(entity, dto);
            _context.Courses.Update(updated);
            await _context.SaveChangesAsync();

            return new ResponseDto<CourseActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = new CourseActionResponseDto { Id = id }
            };
        }

        public async Task<ResponseDto<CourseActionResponseDto>> DeleteAsync(string id)
        {
            var entity = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (entity is null)
            {
                return new ResponseDto<CourseActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            _context.Courses.Remove(entity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CourseActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = new CourseActionResponseDto { Id = id }
            };
        }
    }
}