using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DataSeeder
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public DataSeeder(UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;

    }
    public async Task Seed()
    {
        await SeedRoles();
        await SeedUsers();
    }
    public async Task SeedRoles()
    {
        var roles = new List<IdentityRole>()
        {
            new() {Name = "Admin"},
        };

        if (!await _roleManager.Roles.AnyAsync())
        {
            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }
        }
    }
    public async Task SeedUsers()
    {
        var users = new List<(IdentityUser user, string role)>()
        {
            (new() {UserName = "admin1", Email= "admin1@test.com", EmailConfirmed = true},"Admin"),     
            (new() {UserName = "admin2", Email= "admin2@test.com", EmailConfirmed = true},"Admin"),

        };

        foreach (var (user, role) in users)
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName!);

            if (existingUser == null)
            {
                var result = await _userManager.CreateAsync(user, "Pa$$w0rd");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
