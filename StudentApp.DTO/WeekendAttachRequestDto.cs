namespace StudentApp.DTO;

public class WeekendAttachRequestDto
{
    public Guid StudentId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}