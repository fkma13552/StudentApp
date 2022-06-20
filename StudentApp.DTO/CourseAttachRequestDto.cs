namespace StudentApp.DTO;

public class CourseAttachRequestDto
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
