using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class StudentsController : BaseApiController
{
    private readonly IStudentsService _studentsService;

    public StudentsController( IStudentsService studentsService)
    {
         _studentsService = studentsService;  
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllStudentsAsync([FromQuery] PaginationParams p, [FromQuery] string? status = null)
    {
        var result = await _studentsService.GetAllStudentsAsync(p,status);
        return HandleResult(result);
    }
    [HttpGet("count")]
    public async Task<IActionResult> GetAllStudentsCountAsync([FromQuery] string? status = null)
    {
        var result = await _studentsService.GetAllStudentsCountAsync(status);
        return HandleResult(result);
    }
    [HttpGet("id")]
    public async Task<IActionResult> GetStudentByIdAsync(int id)
    {
        var result = await _studentsService.GetStudentByIdAsync(id);
        return HandleResult(result);
    }
    [HttpGet("email")]
    public async Task<IActionResult> GetStudentByEmailAsync(string email)
    {
        var result = await _studentsService.GetStudentByEmailAsync(email);
        return HandleResult(result);
    }
    [HttpPost]
    public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentDto dto)
    {
        var result = await _studentsService.AddStudentAsync(dto);
        return HandleResult(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudentDto dto)
    {
        var result = await _studentsService.UpdateStudentAsync(dto);
        return HandleResult(result);
    }
}
