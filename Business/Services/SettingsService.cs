namespace Business.Services;

public class SettingsService : ISettingsService
{
    private readonly ISettingsRepository _settingsRepository;
    public SettingsService(ISettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
    }
    public async Task<Result<string>> IsDbConnected()
    {
        bool isConnected = await _settingsRepository.IsDbConnected();
        return isConnected ? Result<string>.Success("Connected") : Result<string>.Failure("Not Connected", 400);
    }
}
