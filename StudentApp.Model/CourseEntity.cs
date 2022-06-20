using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Model;

public class CourseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public IList<StudentCourseEntity> StudentCourses { get; set; }
}