using CursosApp.Constants;
using CursosApp.Database;
using CursosApp.Dtos.Categories;
using CursosApp.Dtos.Common;
using CursosApp.Mappers;
using CursosApp.Services.Categories;
using Microsoft.EntityFrameworkCore;


namespace CursosApp.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var entities = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            return new ResponseDto<List<CategoryDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = CategoryMapper.ListEntityToListDto(entities)
            };
        }

        public async Task<ResponseDto<CategoryDto>> GetOneByIdAsync(string id)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (entity is null)
            {
                return new ResponseDto<CategoryDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            return new ResponseDto<CategoryDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = CategoryMapper.EntityToDto(entity)
            };
        }


    }
}