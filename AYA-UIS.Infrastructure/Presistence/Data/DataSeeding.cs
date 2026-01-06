using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Shared.Dtos.Auth_Module;

public class DataSeeding : IDataSeeding
{
    private readonly AYA_UIS_InfoDbContext _dbContext;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public DataSeeding(
        AYA_UIS_InfoDbContext dbContext,
        RoleManager<IdentityRole> roleManager,
        UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedDataInfoAsync()
    {
        var pendingMigration = await _dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigration.Any())
            await _dbContext.Database.MigrateAsync();
    }

    public async Task SeedIdentityDataAsync()
    {
        string[] roleNames = { "Admin", "Instructor", "Student" };
        foreach (var roleName in roleNames)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole { Name = roleName };
                await _roleManager.CreateAsync(role);
            }

            if (!_userManager.Users.Any())
            {
                var adminUser = new User()
                {
                    DisplayName = "Moustafa Ezzat",
                    Email = "MoustafaEzzat@gmail.com",
                    UserName = "Moustafa02",
                    PhoneNumber = "01557703382",
                    Academic_Code = "2203071"

                };
                await _userManager.CreateAsync(adminUser, "Moustafa@123");
                await _userManager.AddToRoleAsync(adminUser, "Admin");

            }

        }
    }
}
