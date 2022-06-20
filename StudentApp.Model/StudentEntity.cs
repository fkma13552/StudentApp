namespace StudentApp.Model;

public class StudentEntity
{
    public Guid Id { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public IList<StudentCourseEntity> StudentCourses { get; set; }
    public IList<WeekendEntity> Weekends { get; set; }
}