namespace StudentApp.Domain;

public class Weekend
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}