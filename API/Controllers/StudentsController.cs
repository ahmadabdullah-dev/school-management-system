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
    public async Task<IActionResult> GetAllStudentsAsync([FromQuery] PaginationParams p)
    {
        var result = await _studentsService.GetAllStudentsAsync(p);
        return HandleResult(result);
    }
}
