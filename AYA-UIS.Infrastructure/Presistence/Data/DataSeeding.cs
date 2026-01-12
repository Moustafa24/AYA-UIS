using Domain.Contracts;
using Domain.Entities.Identity;
using Domain.Entities.Info_Module;
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
        try
        {
            var pendingMigration = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigration.Any())
                await _dbContext.Database.MigrateAsync();


            // ================= Grade Years =================
            if (!_dbContext.GradeYears.Any())
            {
                var gradeYears = new List<GradeYear>
        {
            new() { Name = "First Year" },
            new() { Name = "Second Year" },
            new() { Name = "Third Year" },
            new() { Name = "Fourth Year" },
            new() { Name = "Fifth Year" } // للهندسة فقط
        };

                await _dbContext.GradeYears.AddRangeAsync(gradeYears);
                await _dbContext.SaveChangesAsync();
            }

            // ================= Departments =================
            if (!_dbContext.Departments.Any())
            {
                var departments = new List<Department>
        {
            new() { Name = "Computer Science" },
            new() {  Name = "Business English" },
            new() { Name = "Business Arabic" },
            new() { Name = "Journalism" },
            new() { Name = "Engineering" }
        };

                await _dbContext.Departments.AddRangeAsync(departments);
                await _dbContext.SaveChangesAsync();
            }

            // ================= Department Fees =================
            if (!_dbContext.DepartmentFees.Any())
            {
                var fees = new List<DepartmentFee>
        {
            // CS (4 years)
            new() { DepartmentId = 1, GradeYearId = 1, FeeAmount = 6000 },
            new() { DepartmentId = 1, GradeYearId = 2, FeeAmount = 6500 },
            new() { DepartmentId = 1, GradeYearId = 3, FeeAmount = 7000 },
            new() { DepartmentId = 1, GradeYearId = 4, FeeAmount = 7500 },

            // Business English (4 years)
            new() { DepartmentId = 2, GradeYearId = 1, FeeAmount = 5000 },
            new() { DepartmentId = 2, GradeYearId = 2, FeeAmount = 5200 },
            new() { DepartmentId = 2, GradeYearId = 3, FeeAmount = 5400 },
            new() { DepartmentId = 2, GradeYearId = 4, FeeAmount = 5600 },

            // Business Arabic (4 years)
            new() { DepartmentId = 3, GradeYearId = 1, FeeAmount = 4500 },
            new() { DepartmentId = 3, GradeYearId = 2, FeeAmount = 4700 },
            new() { DepartmentId = 3, GradeYearId = 3, FeeAmount = 4900 },
            new() { DepartmentId = 3, GradeYearId = 4, FeeAmount = 5100 },

            // Journalism (4 years)
            new() { DepartmentId = 4, GradeYearId = 1, FeeAmount = 4800 },
            new() { DepartmentId = 4, GradeYearId = 2, FeeAmount = 5000 },
            new() { DepartmentId = 4, GradeYearId = 3, FeeAmount = 5200 },
            new() { DepartmentId = 4, GradeYearId = 4, FeeAmount = 5400 },

            // Engineering (5 years 👇)
            new() { DepartmentId = 5, GradeYearId = 1, FeeAmount = 7000 },
            new() { DepartmentId = 5, GradeYearId = 2, FeeAmount = 7500 },
            new() { DepartmentId = 5, GradeYearId = 3, FeeAmount = 8000 },
            new() { DepartmentId = 5, GradeYearId = 4, FeeAmount = 8500 },
            new() { DepartmentId = 5, GradeYearId = 5, FeeAmount = 9000 }
        };

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
