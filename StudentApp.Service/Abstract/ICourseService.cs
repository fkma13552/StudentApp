using StudentApp.Domain;
using StudentApp.Model;
using Course = StudentApp.Domain.Course;
using StudentCourse = StudentApp.Domain.StudentCourse;

namespace StudentApp.Service.Abstract;

public interface ICourseService
{
    Task<Model.CourseEntity> Create(Course course);
    Task<IEnumerable<Model.CourseEntity>> GetAll();
    Task<bool> AttachToStudent(StudentCourse studentCourse);
}