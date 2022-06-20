using StudentApp.Model;

namespace StudentApp.DAL.Abstract;

public interface ICourseRepository : IRepository<CourseEntity, Guid>
{
    Task<IEnumerable<CourseEntity>> GetAll();
}