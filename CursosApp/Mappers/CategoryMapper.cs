using CursosApp.Dtos.Categories;
using CursosApp.Entities;


namespace CursosApp.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto EntityToDto(CategoryEntity entity)
        {
            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        public static List<CategoryDto> ListEntityToListDto(List<CategoryEntity> entities)
        {
            return entities.Select(EntityToDto).ToList();
        }


    }
}