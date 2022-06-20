using StudentApp.Domain;
using StudentApp.Model;
using Student = StudentApp.Domain.Student;

namespace StudentApp.Service.Abstract;

public interface IStudentService
{
    Task<Model.StudentEntity> Create(Student student);
    Task<IEnumerable<Model.StudentEntity>> GetAll();
    Task<Model.StudentEntity> GetById(Guid id);
}