using Microsoft.EntityFrameworkCore;
using StudentApp.DAL.Abstract;
using StudentApp.Model;

namespace StudentApp.DAL.Repositories;

public class StudentRepository : RepositoryBase<StudentEntity, Guid>, IStudentRepository
{
    public StudentRepository(StudentContext context) : base(context)
    {
    }

    public async Task<StudentEntity> GetByIdWithNestedProperties(Guid id)
    {
        var entity = await dbSet
            .Include(s => s.StudentCourses)
            .ThenInclude(c => c.Course)
            .Include(s => s.Weekends)
            .FirstOrDefaultAsync(s => s.Id == id);
        return entity;
            
    }
    public async Task<IEnumerable<StudentEntity>> GetAll()
    {
        return await dbSet.ToListAsync();
    }
}