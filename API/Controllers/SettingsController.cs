using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles =("Admin"))]
public class SettingsController : BaseApiController
{
    private readonly ISettingsService _settingsService;
    public SettingsController(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    [HttpGet("is-db-connected")]
    public async Task<IActionResult> IsDbConnected()
    {
        var result = await _settingsService.IsDbConnected();
        return HandleResult(result);
    }
}
