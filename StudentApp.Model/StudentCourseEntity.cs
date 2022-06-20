using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Model;

public class StudentCourseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [ForeignKey("StudentEntity")]
    public Guid StudentId { get; set; }
    
    [ForeignKey("CourseEntity")]
    public Guid CourseId { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public CourseEntity Course { get; set; }
    public StudentEntity Student { get; set; }
}