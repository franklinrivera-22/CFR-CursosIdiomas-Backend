using CursosApp.Dtos.Courses;

namespace CursosApp.Dtos.Enrollments
{
    public class EnrollmentDto
    {
        public string Id { get; set; }
        public int Progress { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public CourseDto Course { get; set; }
    }
}