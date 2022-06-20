using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Model;

public class WeekendEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [ForeignKey("StudentEntity")]
    public Guid StudentId { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}