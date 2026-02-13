using AYA_UIS.Core.Domain.Entities.Identity;
using AYA_UIS.Core.Domain.Entities.Models;
using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Identity;
using Shared.Dtos.Auth_Module;
using AYA_UIS.Core.Domain.Enums;

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

                // Seed actual calendar study years from 2018-2019 up to 2025-2026
                int startFrom = 2018;
                int endAt = 2025; // last StartYear → 2025-2026

                foreach (var dept in departments)
                {
                    for (int year = startFrom; year <= endAt; year++)
                    {
                        studyYears.Add(new StudyYear
                        {
                            StartYear = year,
                            EndYear = year + 1,
                            DepartmentId = dept.Id
                        });
                    }
                }

                await _dbContext.StudyYears.AddRangeAsync(studyYears);
                await _dbContext.SaveChangesAsync();
            }

            // ================= Semesters =================
            if (!_dbContext.Semesters.Any())
            {
                var departments = await _dbContext.Departments.ToListAsync();
                var semesters = new List<Semester>();

                foreach (var dept in departments)
                {
                    var deptStudyYears = await _dbContext.StudyYears
                        .Where(sy => sy.DepartmentId == dept.Id)
                        .ToListAsync();

                    foreach (var studyYear in deptStudyYears)
                    {
                        // Semester1 (Fall) — Sep to Dec of StartYear
                        semesters.Add(new Semester
                        {
                            Title = SemesterEnums.Semester1,
                            StartDate = new DateTime(studyYear.StartYear, 9, 1),
                            EndDate = new DateTime(studyYear.StartYear, 12, 31),
                            DepartmentId = dept.Id
                        });

                        // Semester2 (Spring) — Jan to May of EndYear
                        semesters.Add(new Semester
                        {
                            Title = SemesterEnums.Semester2,
                            StartDate = new DateTime(studyYear.EndYear, 1, 1),
                            EndDate = new DateTime(studyYear.EndYear, 5, 31),
                            DepartmentId = dept.Id
                        });
                    }
                }

                await _dbContext.Semesters.AddRangeAsync(semesters);
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

            // ================= Courses =================
            if (!_dbContext.Courses.Any())
            {
                var csDepartment = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Code == "CS");
                if (csDepartment != null)
                {
                    var courses = new List<Course>
                    {
                        // Year 1
                        new Course { Code = "CS101", Name = "Introduction to Computer Science", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS102", Name = "Programming Fundamentals I", Credits = 4, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS103", Name = "Programming Fundamentals II", Credits = 4, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS104", Name = "Computer Organization", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "MATH101", Name = "Discrete Mathematics", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "MATH102", Name = "Calculus I", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "ENG101", Name = "English Communication", Credits = 2, DepartmentId = csDepartment.Id },
                        new Course { Code = "PHY101", Name = "Physics I", Credits = 3, DepartmentId = csDepartment.Id },

                        // Year 2
                        new Course { Code = "CS201", Name = "Data Structures", Credits = 4, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS202", Name = "Algorithms", Credits = 4, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS203", Name = "Object-Oriented Programming", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS204", Name = "Database Systems", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS205", Name = "Web Development", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "MATH201", Name = "Linear Algebra", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "STAT201", Name = "Statistics", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "ENG201", Name = "Technical Writing", Credits = 2, DepartmentId = csDepartment.Id },

                        // Year 3
                        new Course { Code = "CS301", Name = "Software Engineering", Credits = 4, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS302", Name = "Operating Systems", Credits = 4, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS303", Name = "Computer Networks", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS304", Name = "Artificial Intelligence", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS305", Name = "Machine Learning", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS306", Name = "Cybersecurity", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS307", Name = "Mobile App Development", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "MATH301", Name = "Numerical Methods", Credits = 3, DepartmentId = csDepartment.Id },

                        // Year 4
                        new Course { Code = "CS401", Name = "Advanced Algorithms", Credits = 4, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS402", Name = "Distributed Systems", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS403", Name = "Computer Graphics", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS404", Name = "Big Data Analytics", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS405", Name = "Cloud Computing", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS406", Name = "Blockchain Technology", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS407", Name = "IoT and Embedded Systems", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS408", Name = "Capstone Project I", Credits = 4, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS409", Name = "Capstone Project II", Credits = 4, DepartmentId = csDepartment.Id },
                        new Course { Code = "BUS401", Name = "Entrepreneurship", Credits = 2, DepartmentId = csDepartment.Id },

                        // Elective Courses
                        new Course { Code = "CS501", Name = "Advanced Machine Learning", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS502", Name = "Deep Learning", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS503", Name = "Natural Language Processing", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS504", Name = "Computer Vision", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS505", Name = "Quantum Computing", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS506", Name = "Game Development", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS507", Name = "DevOps", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS508", Name = "Ethical Hacking", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS509", Name = "Data Mining", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS510", Name = "Parallel Computing", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS511", Name = "Compiler Design", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS512", Name = "Human-Computer Interaction", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS513", Name = "Software Testing", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS514", Name = "Cryptography", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS515", Name = "Augmented Reality", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS516", Name = "Virtual Reality", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS517", Name = "Bioinformatics", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS518", Name = "Robotics", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS519", Name = "Digital Signal Processing", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS520", Name = "Information Retrieval", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS521", Name = "Network Security", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS522", Name = "Advanced Databases", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS523", Name = "Microservices Architecture", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS524", Name = "Serverless Computing", Credits = 3, DepartmentId = csDepartment.Id },
                        new Course { Code = "CS525", Name = "Edge Computing", Credits = 3, DepartmentId = csDepartment.Id }
                    };

                    await _dbContext.Courses.AddRangeAsync(courses);
                    await _dbContext.SaveChangesAsync();
                }
            }

            // ================= Course Prerequisites =================
            if (!_dbContext.CoursePrerequisites.Any())
            {
                var csDepartment = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Code == "CS");
                if (csDepartment != null)
                {
                    var courses = await _dbContext.Courses.Where(c => c.DepartmentId == csDepartment.Id).ToDictionaryAsync(c => c.Code, c => c.Id);
                    
                    var prerequisites = new List<CoursePrerequisite>();
                    
                    // Only add prerequisites for courses that exist in the database
                    var prerequisiteDefinitions = new[]
                    {
                        new { CourseCode = "CS103", PrereqCode = "CS102" },
                        new { CourseCode = "CS201", PrereqCode = "CS103" },
                        new { CourseCode = "CS202", PrereqCode = "CS201" },
                        new { CourseCode = "CS203", PrereqCode = "CS102" },
                        new { CourseCode = "CS204", PrereqCode = "CS103" },
                        new { CourseCode = "CS401", PrereqCode = "CS202" },
                        new { CourseCode = "CS301", PrereqCode = "CS201" },
                        new { CourseCode = "CS302", PrereqCode = "CS201" },
                        new { CourseCode = "CS303", PrereqCode = "CS201" },
                        new { CourseCode = "CS304", PrereqCode = "CS201" },
                        new { CourseCode = "CS305", PrereqCode = "CS304" },
                        new { CourseCode = "CS306", PrereqCode = "CS201" },
                        new { CourseCode = "CS408", PrereqCode = "CS301" },
                        new { CourseCode = "CS408", PrereqCode = "CS302" },
                        new { CourseCode = "CS409", PrereqCode = "CS408" },
                        new { CourseCode = "CS501", PrereqCode = "CS305" },
                        new { CourseCode = "CS502", PrereqCode = "CS501" },
                        new { CourseCode = "CS503", PrereqCode = "CS305" },
                        new { CourseCode = "CS504", PrereqCode = "CS305" },
                        new { CourseCode = "CS508", PrereqCode = "CS306" },
                        new { CourseCode = "CS521", PrereqCode = "CS306" },
                        new { CourseCode = "CS522", PrereqCode = "CS204" }
                    };

                    foreach (var prereq in prerequisiteDefinitions)
                    {
                        if (courses.ContainsKey(prereq.CourseCode) && courses.ContainsKey(prereq.PrereqCode))
                        {
                            prerequisites.Add(new CoursePrerequisite 
                            { 
                                CourseId = courses[prereq.CourseCode], 
                                PrerequisiteCourseId = courses[prereq.PrereqCode] 
                            });
                        }
                    }

                    if (prerequisites.Any())
                    {
                        await _dbContext.CoursePrerequisites.AddRangeAsync(prerequisites);
                        await _dbContext.SaveChangesAsync();
                    }
                }
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
