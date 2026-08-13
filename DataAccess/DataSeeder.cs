using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace DataAccess;

public class DataSeeder
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _appDbContext;
    public DataSeeder(UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        AppDbContext appDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _appDbContext = appDbContext;

    }
    public async Task Seed()
    {
        await SeedRoles();
        await SeedUsers();
        await SeedInstructors();
        await SeedCourses();
        await SeedStudents();
        await SeedStudentProfiles();
        await SeedEnrollments();
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
    public async Task SeedEnrollments()
    {
        var studentIds = await _appDbContext.Students.Select(s => s.StudentId).ToListAsync();
        var courseIds = await _appDbContext.Courses.Select(c => c.CourseId).ToListAsync();
        if (!studentIds.Any() || !courseIds.Any()) return; 

        var statuses = new[] { "InProgress", "Completed", "Dropped" };
        var random = new Random();

        var enrollmentFaker = new Faker<Enrollment>()
            .RuleFor(e => e.StudentId, f => f.PickRandom(studentIds))
            .RuleFor(e => e.CourseId, f => f.PickRandom(courseIds))
            .RuleFor(e => e.EnrollmentDate, f => DateTime.UtcNow.AddDays(-f.Random.Int(1, 300)))
            .RuleFor(e => e.Status, f => f.PickRandom(statuses))
            .RuleFor(e => e.ProgressPercent, (f, e) => e.Status == "Completed" ? 100 : f.Random.Decimal(0, 99))
            .RuleFor(e => e.CompletionDate, (f, e) => e.Status == "Completed"
                ? e.EnrollmentDate.AddDays(f.Random.Int(10, 90))
                : null)
            .RuleFor(e => e.FinalGrade, (f, e) => e.Status == "Completed"
                ? f.Random.Decimal(50, 100)
                : null);

        var enrollments = enrollmentFaker.Generate(300);

        var seen = new HashSet<(int, int)>();
        foreach (var enrollment in enrollments)
        {
            var key = (enrollment.StudentId, enrollment.CourseId);
            if (seen.Add(key))
                await _appDbContext.Enrollments.AddAsync(enrollment);
        }

        await _appDbContext.SaveChangesAsync();
    }
    public async Task SeedInstructors()
    {
        var instructorFaker = new Faker<Instructor>()
            .RuleFor(i => i.FirstName, f => f.Name.FirstName())
            .RuleFor(i => i.LastName, f => f.Name.LastName())
            .RuleFor(i => i.Email, (f, i) => f.Internet.Email(i.FirstName, i.LastName))
            .RuleFor(i => i.HireDate, f => DateOnly.FromDateTime(f.Date.Past(15)))
            .RuleFor(i => i.Salary, f => f.Random.Decimal(40000, 120000))
            .RuleFor(i => i.IsActive, f => f.Random.Bool(0.9f))
            .RuleFor(i => i.ManagerId, f => null); 

        var instructors = instructorFaker.Generate(10);

        foreach (var instructor in instructors)
        {
            var exists = await _appDbContext.Instructors.AnyAsync(x => x.Email == instructor.Email);
            if (!exists)
                await _appDbContext.Instructors.AddAsync(instructor);
        }

        await _appDbContext.SaveChangesAsync();

        var saved = await _appDbContext.Instructors.ToListAsync();
        var random = new Random();

        foreach (var instructor in saved)
        {
            if (random.NextDouble() < 0.4)
            {
                var possibleManagers = saved.Where(x => x.InstructorId != instructor.InstructorId).ToList();
                instructor.ManagerId = possibleManagers[random.Next(possibleManagers.Count)].InstructorId;
            }
        }

        await _appDbContext.SaveChangesAsync();
    }
    public async Task SeedCourses()
    {
        var instructorIds = await _appDbContext.Instructors.Select(i => i.InstructorId).ToListAsync();
        if (!instructorIds.Any()) return; 

        var levels = new[] { "Beginner", "Intermediate", "Advanced" };
        var statuses = new[] { "Draft", "Published", "Archived" };

        var courseFaker = new Faker<Course>()
            .RuleFor(c => c.Title, f => f.Commerce.ProductName())
            .RuleFor(c => c.Code, (f, c) => f.Random.Replace("???-###").ToUpper())
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Price, f => f.Random.Decimal(19, 499))
            .RuleFor(c => c.Level, f => f.PickRandom(levels))
            .RuleFor(c => c.DurationHours, f => f.Random.Int(1, 60))
            .RuleFor(c => c.CreatedAt, f => f.Date.Past(2))
            .RuleFor(c => c.Status, f => f.PickRandom(statuses))
            .RuleFor(c => c.PublishedAt, (f, c) => c.Status == "Published" ? f.Date.Past(1) : null)
            .RuleFor(c => c.InstructorId, f => f.PickRandom(instructorIds));

        var courses = courseFaker.Generate(10);

        foreach (var course in courses)
        {
            var exists = await _appDbContext.Courses.AnyAsync(x => x.Code == course.Code);
            if (!exists)
                await _appDbContext.Courses.AddAsync(course);
        }

        await _appDbContext.SaveChangesAsync();
    }
    public async Task SeedStudents()
    {
        var statuses = new[] { "Active", "Inactive", "Suspended", "Graduated" };

        var studentFaker = new Faker<Student>()
            .RuleFor(s => s.FirstName, f => f.Name.FirstName())
            .RuleFor(s => s.LastName, f => f.Name.LastName())
            .RuleFor(s => s.Email, (f, s) => f.Internet.Email(s.FirstName, s.LastName))
            .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber("+1 ### ### ####"))
            .RuleFor(s => s.Status, f => f.PickRandom(statuses))
            .RuleFor(s => s.RegisteredAt, f => DateTime.UtcNow.AddDays(-f.Random.Int(1, 365)))
            .RuleFor(s => s.DateOfBirth, f => DateOnly.FromDateTime(
                DateTime.UtcNow.AddYears(-f.Random.Int(18, 30)).AddDays(-f.Random.Int(0, 365))));

        var students = studentFaker.Generate(100);

        foreach (var student in students)
        {
            var exists = await _appDbContext.Students.AnyAsync(x => x.Email == student.Email);
            if (!exists)
                await _appDbContext.Students.AddAsync(student);
        }

        await _appDbContext.SaveChangesAsync();
    }
    public async Task SeedStudentProfiles()
    {
        var studentIds = await _appDbContext.Students
            .Where(s => s.StudentProfile == null)
            .Select(s => s.StudentId)
            .ToListAsync();

        var profileFaker = new Faker<StudentProfile>()
            .RuleFor(p => p.Address, f => f.Address.StreetAddress())
            .RuleFor(p => p.City, f => f.Address.City())
            .RuleFor(p => p.Country, f => f.Address.Country())
            .RuleFor(p => p.Bio, f => f.Lorem.Sentence(15))
            .RuleFor(p => p.LinkedInUrl, f => f.Internet.Url());

        foreach (var studentId in studentIds)
        {
            var profile = profileFaker.Generate();
            profile.StudentId = studentId; 
            await _appDbContext.StudentProfiles.AddAsync(profile);
        }

        await _appDbContext.SaveChangesAsync();
    }
}
