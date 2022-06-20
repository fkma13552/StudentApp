using Microsoft.AspNetCore.Mvc;
using StudentApp.DTO;
using StudentApp.Extensions;
using StudentApp.Service.Abstract;

namespace StudentApp.WebApi.Controllers;

[Route("api/[controller]/[action]")]
public class WeekendController : ControllerBase
{
    private readonly IWeekendService _weekendService;

    public WeekendController(IWeekendService weekendService)
    {
        _weekendService = weekendService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AttachToStudent([FromBody]WeekendAttachRequestDto weekend)
    {
        if (weekend.StartDate.DayOfWeek != DayOfWeek.Monday)
        {
           return BadRequest("Start date to be Monday is obligatory");
        }
        
        if (weekend.EndDate.DayOfWeek != DayOfWeek.Friday)
        {
            return BadRequest("End date to be Friday is obligatory");
        }
        
        if (weekend.EndDate < weekend.StartDate)
        {
            return BadRequest("End date can not be before start date");
        }
        var entity = await _weekendService.AttachToStudent(weekend.ToDomain());
        return Ok(entity);
    }
}