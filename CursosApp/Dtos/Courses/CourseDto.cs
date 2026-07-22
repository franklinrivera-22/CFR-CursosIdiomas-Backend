
using CursosApp.Dtos.Categories;

namespace CursosApp.Dtos.Courses
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public decimal Price { get; set; }
        public int DurationHours { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public CategoryOneDto Category { get; set; }
    }
}