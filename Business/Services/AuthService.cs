using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AuthService : IAuthService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Result<string>> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.email);

        if (user == null)
            return Result<string>.Failure("Invalid email or password", 401);

        var loginResult = await _signInManager.PasswordSignInAsync(user, dto.password, dto.isPersistence, true);
    
        if (!loginResult.Succeeded)
            return Result<string>.Failure("Invalid email or password", 401);

        return Result<string>.Success("Logged in successfully");
    }
    public async Task<Result<string>> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return Result<string>.Success("Logged out successfully");
    }

}
