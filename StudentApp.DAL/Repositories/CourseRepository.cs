using Microsoft.EntityFrameworkCore;
using StudentApp.DAL.Abstract;
using StudentApp.Model;

namespace StudentApp.DAL.Repositories;

public class CourseRepository : RepositoryBase<CourseEntity, Guid>, ICourseRepository
{
    public CourseRepository(StudentContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CourseEntity>> GetAll()
    {
        return await dbSet.ToListAsync();
    }
}