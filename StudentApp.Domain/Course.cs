namespace StudentApp.Domain;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public IList<StudentCourse> StudentCourses { get; set; }
}