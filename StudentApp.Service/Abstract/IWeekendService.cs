using StudentApp.Domain;
using StudentApp.DTO;
using StudentApp.Model;
using Weekend = StudentApp.Domain.Weekend;

namespace StudentApp.Service.Abstract;

public interface IWeekendService
{
    Task<bool> AttachToStudent(Weekend weekend);
}