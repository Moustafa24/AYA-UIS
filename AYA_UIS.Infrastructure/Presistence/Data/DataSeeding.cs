using AYA_UIS.Core.Domain.Entities.Identity;
using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
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


            // ================= Study Years =================
            if (!_dbContext.StudyYears.Any())
            {
                var studyYears = new List<StudyYear>
        {
            new() { Year = 1 },
            new() { Year = 2 },
            new() { Year = 3 },
            new() { Year = 4 },
            new() { Year = 5 } // للهندسة فقط
        };

                await _dbContext.StudyYears.AddRangeAsync(studyYears);
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
            new() { DepartmentId = 1, StudyYearId = 1 },
            new() { DepartmentId = 1, StudyYearId = 2 },
            new() { DepartmentId = 1, StudyYearId = 3 },
            new() { DepartmentId = 1, StudyYearId = 4 },

            // Business English (4 years)
            new() { DepartmentId = 2, StudyYearId = 1 },
            new() { DepartmentId = 2, StudyYearId = 2 },
            new() { DepartmentId = 2, StudyYearId = 3 },
            new() { DepartmentId = 2, StudyYearId = 4 },

            // Business Arabic (4 years)
            new() { DepartmentId = 3, StudyYearId = 1 },
            new() { DepartmentId = 3, StudyYearId = 2 },
            new() { DepartmentId = 3, StudyYearId = 3 },
            new() { DepartmentId = 3, StudyYearId = 4 },

            // Journalism (4 years)
            new() { DepartmentId = 4, StudyYearId = 1 },
            new() { DepartmentId = 4, StudyYearId = 2 },
            new() { DepartmentId = 4, StudyYearId = 3 },
            new() { DepartmentId = 4, StudyYearId = 4 },

            // Engineering (5 years)
            new() { DepartmentId = 5, StudyYearId = 1 },
            new() { DepartmentId = 5, StudyYearId = 2 },
            new() { DepartmentId = 5, StudyYearId = 3 },
            new() { DepartmentId = 5, StudyYearId = 4 },
            new() { DepartmentId = 5, StudyYearId = 5 }
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
