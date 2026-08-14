using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

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
            .Where(status != null ?  s => s.Status == status : s => s.Status != null)
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

}

