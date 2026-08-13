using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController :BaseApiController
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return HandleResult(result);
    }
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var result = await _authService.LogoutAsync();
        return HandleResult(result);
    }
}
