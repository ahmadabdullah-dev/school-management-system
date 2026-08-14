using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class StudentsRepository : IStudentRepository
{
    private readonly AppDbContext _appDbContext;
    public StudentsRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<PagedList<StudentProjection>> GetAllStudentsAsync(PaginationParams p)
    {
        var query = _appDbContext.Students
            .AsNoTracking()
            .Select(x => new StudentProjection
            {
                StudentId = x.StudentId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                DateOfBirth= x.DateOfBirth,
                RegisteredAt = x.RegisteredAt,
                Status = x.Status,
            });

        return await PagedList<StudentProjection>.CreateAsync(query, p.Page, p.PageSize);
    }
}

