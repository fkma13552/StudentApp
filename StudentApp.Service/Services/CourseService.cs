using StudentApp.DAL.Abstract;
using StudentApp.Domain;
using StudentApp.Extensions;
using StudentApp.Model;
using StudentApp.Service.Abstract;

namespace StudentApp.Service.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;

    public CourseService(ICourseRepository courseRepository, IStudentRepository studentRepository)
    {
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
    }

    public async Task<CourseEntity> Create(Course course)
    {
        course.Id = Guid.NewGuid();
        var courseEntity = await _courseRepository.Create(course.ToModel());
        _courseRepository.SaveChanges();
        return courseEntity;
    }

    public async Task<IEnumerable<CourseEntity>> GetAll()
    {
        return await _courseRepository.GetAll();
    }

    public async Task<bool> AttachToStudent(StudentCourse studentCourse)
    {
        var student = await _studentRepository.GetByIdWithNestedProperties(studentCourse.Student.Id);
        var course = await _courseRepository.GetById(studentCourse.Course.Id);
        
        if (course is not null && student is not null)
        {
            var coursesCountBefore = student.StudentCourses.Count;
            //student.StudentCourses ??= new List<StudentCourseEntity>();
            var sce = studentCourse.ToModel();
            student.StudentCourses.Add(sce);

            if (!CheckCoursesOverlapping(student))
            {
                _courseRepository.SaveChanges();
                return student.StudentCourses.Count > coursesCountBefore;
            }
        }
        
        return false;
    }

    private bool CheckCoursesOverlapping(StudentEntity student)
    {
        if (student.StudentCourses.Count < 2 && student.Weekends.Count == 0)
        {
            return false;
        }
        
        var sorted = student.StudentCourses.OrderBy(s => s.StartDate).ToList();
        for (int i = sorted.Count - 1; i >= 1; i--)
        {
            if (Utils.HasOverlap(
                    student.StudentCourses[i].StartDate, 
                    student.StudentCourses[i].StartDate, 
                    student.StudentCourses[i-1].StartDate,
                    student.StudentCourses[i-1].EndDate))
            {
                return true;
            }
        }

        return false;
    }
}