using StudentApp.DAL.Abstract;
using StudentApp.Domain;
using StudentApp.Model;
using StudentApp.Service.Abstract;
using StudentApp.Extensions;

namespace StudentApp.Service.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentEntity> Create(Student student)
    {
        student.Id = Guid.NewGuid();
        var studentEntity = await _studentRepository.Create(student.ToModel());
        _studentRepository.SaveChanges();
        return studentEntity;
    }

    public async Task<IEnumerable<StudentEntity>> GetAll()
    {
        return await _studentRepository.GetAll();
    }

    public async Task<StudentEntity> GetById(Guid id)
    {
        return await _studentRepository.GetByIdWithNestedProperties(id);
    }
}