using StudentApp.DAL.Abstract;
using StudentApp.Model;

namespace StudentApp.DAL.Repositories;

public class WeekendRepository : RepositoryBase<WeekendEntity, Guid>, IWeekendRepository
{
    public WeekendRepository(StudentContext context) : base(context)
    {
    }
}