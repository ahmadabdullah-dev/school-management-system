using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;
    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    public string? GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    public async Task<Result<CurrentUserDto>> GetCurrentUserAsync()
    {
        var userId = GetCurrentUserId();

        if (string.IsNullOrEmpty(userId)) 
            return Result<CurrentUserDto>.Failure("You must be logged in to perform this action.", 401);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Result<CurrentUserDto>.Failure("User not found!. It may have been removed or deactivated.", 404);

        var userDto = new CurrentUserDto
        {
            Id = user.Id,
            UserName = user.UserName!,
        };
        return Result<CurrentUserDto>.Success(userDto);
    }
}
