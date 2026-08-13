namespace Business.Interfaces;

public interface IUserService
{
    string? GetCurrentUserId();
    Task<Result<CurrentUserDto>> GetCurrentUserAsync();
}
