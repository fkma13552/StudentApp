namespace StudentApp.Domain;

public class StudentCourse
{
    public Guid Id { get; set; }
    public Student? Student { get; set; }
    public Course? Course{ get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}