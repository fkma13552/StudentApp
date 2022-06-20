using StudentApp.DAL.Abstract;
using StudentApp.Domain;
using StudentApp.Extensions;
using StudentApp.Model;
using StudentApp.Service.Abstract;

namespace StudentApp.Service.Services;

public class WeekendService : IWeekendService
{
    private readonly IStudentRepository _studentRepository;

    public WeekendService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<bool> AttachToStudent(Weekend weekend)
    {
        var student = await _studentRepository.GetByIdWithNestedProperties(weekend.StudentId);

        if (student is not null && !CheckWeekendOverlapping(student, weekend))
        {
            var sortedDates = student.StudentCourses
                .OrderBy(s => s.StartDate)
                .ToList();
            var weekendDuration = weekend.EndDate - weekend.StartDate;
            for (int i = 0; i < sortedDates.Count; i++)
            {
                if (Utils.HasOverlap(
                        student.StudentCourses[i].StartDate,
                        student.StudentCourses[i].StartDate,
                        weekend.StartDate,
                        weekend.EndDate))
                {
                    weekend.StartDate = student.StudentCourses[i].EndDate.AddDays(3);
                    weekend.EndDate = student.StudentCourses[i].EndDate.AddDays(weekendDuration.Days + 3);
                    break;
                }
            }

            foreach (var dates in sortedDates.Where(s => s.StartDate >= weekend.StartDate))
            {
                dates.StartDate = dates.StartDate.AddDays(weekendDuration.Days + 3);
                dates.EndDate = dates.EndDate.AddDays(weekendDuration.Days + 3);
            }

            student.StudentCourses = sortedDates;
            student.Weekends.Add(weekend.ToModel());
            _studentRepository.SaveChanges();
            return true;
        }

        return false;
    }
    
    private bool CheckWeekendOverlapping(StudentEntity student, Weekend weekend)
    {
        if (student.Weekends.Count < 1)
        {
            return false;
        }
        
        var sorted = student.Weekends.OrderBy(s => s.StartDate).ToList();
        for (int i = sorted.Count - 1; i >= 1; i--)
        {
            if (Utils.HasOverlap(
                    student.Weekends[i].StartDate, 
                    student.Weekends[i].StartDate, 
                    weekend.StartDate,
                    weekend.EndDate))
            {
                return true;
            }
        }

        return false;
    }
}