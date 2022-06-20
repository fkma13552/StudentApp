using Microsoft.EntityFrameworkCore;
using StudentApp.Model;

namespace StudentApp.DAL;

public class StudentContext : DbContext
{
    public StudentContext()
    { }
    
    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<WeekendEntity> Weekends { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(local);Database=StudentsDb;Trusted_Connection=True;");
    }
}