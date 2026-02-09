using AYA_UIS.Core.Domain.Entities.Identity;
using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Identity;
using Shared.Dtos.Auth_Module;

public class DataSeeding : IDataSeeding
{
    private readonly AYA_UIS_InfoDbContext _dbContext;
    private readonly IdentityAYADbContext _identityDbContext;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public DataSeeding(
        AYA_UIS_InfoDbContext dbContext,
        IdentityAYADbContext identityDbContext,
        RoleManager<IdentityRole> roleManager,
        UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _identityDbContext = identityDbContext;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedDataInfoAsync()
    {
        try
        {
            var pendingMigration = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigration.Any())
                await _dbContext.Database.MigrateAsync();


            // ================= Departments =================
            if (!_dbContext.Departments.Any())
            {
                var departments = new List<Department>
                {
                    new() { Name = "Computer Science", Code = "CS" },
                    new() { Name = "Business English", Code = "BE" },
                    new() { Name = "Business Arabic", Code = "BA" },
                    new() { Name = "Journalism", Code = "JR" },
                    new() { Name = "Engineering", Code = "ENG" }
                };

                await _dbContext.Departments.AddRangeAsync(departments);
                await _dbContext.SaveChangesAsync();
            }

            // ================= Study Years (per department) =================
            if (!_dbContext.StudyYears.Any())
            {
                var departments = await _dbContext.Departments.ToListAsync();
                var studyYears = new List<StudyYear>();

                foreach (var dept in departments)
                {
                    int maxYears = dept.Name == "Engineering" ? 5 : 4;
                    for (int y = 1; y <= maxYears; y++)
                    {
                        studyYears.Add(new StudyYear { Year = y, DepartmentId = dept.Id });
                    }
                }

                await _dbContext.StudyYears.AddRangeAsync(studyYears);
                await _dbContext.SaveChangesAsync();
            }

            // ================= Department Fees =================
            if (!_dbContext.DepartmentFees.Any())
            {
                var studyYears = await _dbContext.StudyYears.ToListAsync();
                var fees = studyYears.Select(sy => new DepartmentFee
                {
                    DepartmentId = sy.DepartmentId,
                    StudyYearId = sy.Id
                }).ToList();

                await _dbContext.DepartmentFees.AddRangeAsync(fees);
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Seeder Error: " + ex.Message);
            throw; // optional: عشان يظهر full exception
        }
    }

    public async Task SeedIdentityDataAsync()
    {
        // Ensure Identity database is migrated
        var pendingMigrations = await _identityDbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
            await _identityDbContext.Database.MigrateAsync();

        // Seed roles
        string[] roleNames = { "Admin", "Instructor", "Student" };
        foreach (var roleName in roleNames)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole { Name = roleName };
                await _roleManager.CreateAsync(role);
            }
        }

        // Seed admin user if no users exist
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
            
            var result = await _userManager.CreateAsync(adminUser, "Moustafa@123");
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
