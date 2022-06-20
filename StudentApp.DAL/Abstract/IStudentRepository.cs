using StudentApp.Model;

namespace StudentApp.DAL.Abstract;

public interface IStudentRepository : IRepository<StudentEntity, Guid>
{
    Task<StudentEntity> GetByIdWithNestedProperties(Guid id);
    Task<IEnumerable<StudentEntity>> GetAll();
}