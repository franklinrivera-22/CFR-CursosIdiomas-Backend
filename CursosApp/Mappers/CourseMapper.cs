
using CursosApp.Dtos.Categories;
using CursosApp.Dtos.Courses;
using CursosApp.Entities;

namespace CursosApp.Mappers
{
    public static class CourseMapper
    {
        public static CourseEntity CreateDtoToEntity(CourseCreateDto dto)
        {
            return new CourseEntity
            {
                Id = Guid.NewGuid().ToString(),
                Title = dto.Title,
                Description = dto.Description,
                Level = dto.Level,
                Price = dto.Price,
                DurationHours = dto.DurationHours,
                ImageUrl = dto.ImageUrl,
                IsActive = dto.IsActive,
                CategoryId = dto.CategoryId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
        }

        public static CourseEntity EditDtoToEntity(CourseEntity entity, CourseEditDto dto)
        {
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Level = dto.Level;
            entity.Price = dto.Price;
            entity.DurationHours = dto.DurationHours;
            entity.ImageUrl = dto.ImageUrl;
            entity.IsActive = dto.IsActive;
            entity.CategoryId = dto.CategoryId;
            entity.UpdatedDate = DateTime.Now;
            return entity;
        }

        public static CourseDto EntityToDto(CourseEntity entity)
        {
            return new CourseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Level = entity.Level,
                Price = entity.Price,
                DurationHours = entity.DurationHours,
                ImageUrl = entity.ImageUrl,
                IsActive = entity.IsActive,
                Category = entity.Category == null ? null : new CategoryOneDto
                {
                    Id = entity.Category.Id,
                    Name = entity.Category.Name
                }
            };
        }

        public static List<CourseDto> ListEntityToListDto(List<CourseEntity> entities)
        {
            return entities.Select(EntityToDto).ToList();
        }
    }
}