using Microsoft.AspNetCore.Mvc;
using StudentApp.Domain;
using StudentApp.DTO;
using StudentApp.Extensions;
using StudentApp.Service.Abstract;

namespace StudentApp.WebApi.Controllers;

[Route("api/[controller]/[action]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CourseCreateRequestDto course)
    {
        var entity = await _courseService.Create(course.ToDomain());
        return Ok(entity);
    }
    
    [HttpPost]
    public async Task<IActionResult> AttachToStudent([FromBody]CourseAttachRequestDto courseAttachRequestDto)
    {
        if (courseAttachRequestDto.StartDate.DayOfWeek != DayOfWeek.Monday)
        {
            return BadRequest("Start date to be Monday is obligatory");
        }
        
        if (courseAttachRequestDto.EndDate.DayOfWeek != DayOfWeek.Friday)
        {
            return BadRequest("End date to be Friday is obligatory");
        }
        
        if (courseAttachRequestDto.EndDate < courseAttachRequestDto.StartDate)
        {
            return BadRequest("End date can not be before start date");
        }
        var entity = await _courseService.AttachToStudent(courseAttachRequestDto.ToDomain());
        return Ok(entity);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entities = await _courseService.GetAll();
        return Ok(entities);
    }
}