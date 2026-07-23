using CursosApp.Dtos.Categories;
using CursosApp.Dtos.Common;

namespace CursosApp.Services.Categories
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDto<CategoryDto>> GetOneByIdAsync(string id);
    }
}