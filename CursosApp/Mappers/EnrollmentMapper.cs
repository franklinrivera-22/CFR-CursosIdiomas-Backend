using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursosApp.Dtos.Enrollments;
using CursosApp.Entities;

namespace CursosApp.Mappers
{
    public class EnrollmentMapper
    {
        public static EnrollmentDto EntityToDto(EnrollmentEntity entity)
        {
            return new EnrollmentDto
            {
                Id = entity.Id,
                Progress = entity.Progress,
                IsActive = entity.IsActive,
                CreatedDate = entity.CreatedDate,
                Course = entity.Course == null
                    ? null
                    : CourseMapper.EntityToDto(entity.Course)
            };
        }

        public static List<EnrollmentDto> ListEntityToListDto(List<EnrollmentEntity> entities)
        {
            return entities.Select(EntityToDto).ToList();
        }
    }
}