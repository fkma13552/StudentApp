using Microsoft.AspNetCore.Mvc;
using StudentApp.Domain;
using StudentApp.DTO;
using StudentApp.Extensions;
using StudentApp.Service.Abstract;

namespace StudentApp.WebApi.Controllers;

[Route("api/[controller]/[action]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]StudentCreateRequestDto student)
    {
        var entity = await _studentService.Create(student.ToDomain());
        return Ok(entity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entities = await _studentService.GetAll();
        return Ok(entities);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entity = await _studentService.GetById(id);
        return Ok(entity);
    }
}