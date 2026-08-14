using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class StudentsRepository : IStudentsRepository
{
    private readonly AppDbContext _appDbContext;
    public StudentsRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<PagedList<StudentProjection>> GetAllStudentsAsync(PaginationParams p, string? status = null)
    {    
        var FilteredQuery = _appDbContext.Students
            .AsNoTracking()
            .Where(s => status == null || s.Status == status)
            .Select(x => new StudentProjection
            {
                StudentId = x.StudentId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email, 
                PhoneNumber = x.PhoneNumber,    
                DateOfBirth = x.DateOfBirth,
                RegisteredAt = x.RegisteredAt,
                Status = x.Status,
            });

        return await PagedList<StudentProjection>.CreateAsync(FilteredQuery, p.Page, p.PageSize);
    }
    public async Task<int> GetAllStudentsCountAsync(string? status = null)
    {
        var studentsCount = await _appDbContext.Students
            .Where(s => status == null || s.Status == status)
            .CountAsync();

        return studentsCount;
    }
    public async Task<StudentProjection?> GetStudentByIdAsync(int id)
    {
        var student = await _appDbContext.Students.FindAsync(id);
       
        if (student == null) 
            return null;

        var projection = new StudentProjection
        {
            StudentId = student.StudentId,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            DateOfBirth = student.DateOfBirth,
            Status = student.Status,
            RegisteredAt = student.RegisteredAt,
        };

        return projection;
    }
<<<<<<< HEAD
    public async Task<StudentProjection?> GetStudentByEmailAsync(string email)
    {
        var student = await _appDbContext.Students.FirstOrDefaultAsync(s => s.Email == email);

        if (student == null)
            return null;

        var projection = new StudentProjection
        {
            StudentId = student.StudentId,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            DateOfBirth = student.DateOfBirth,
            Status = student.Status,
            RegisteredAt = student.RegisteredAt,
        };

        return projection;
    }
=======
>>>>>>> fcda06575dfb768219b1acdb38190f1438b8f530

}

