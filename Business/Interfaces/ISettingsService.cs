namespace Business.Interfaces;

public interface ISettingsService
{
    Task<Result<string>> IsDbConnected();
}
