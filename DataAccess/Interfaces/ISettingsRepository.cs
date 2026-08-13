namespace DataAccess.Interfaces;

public interface ISettingsRepository
{
    Task<bool> IsDbConnected();
}
