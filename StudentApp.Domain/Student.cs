namespace StudentApp.Domain;

public class Student
{
    public Guid Id { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public IList<StudentCourse> StudentCourses { get; set; }
    public IList<Weekend> Weekends { get; set; }
}