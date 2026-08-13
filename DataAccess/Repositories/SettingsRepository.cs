namespace DataAccess.Repositories;

public class SettingsRepository : ISettingsRepository
{
    private readonly AppDbContext _appDbContext;
    public SettingsRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<bool> IsDbConnected()
    {
        bool result = await _appDbContext.Database.CanConnectAsync();
        return result;
    }
}
