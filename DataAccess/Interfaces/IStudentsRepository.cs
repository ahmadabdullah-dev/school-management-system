namespace DataAccess.Interfaces;

public interface IStudentsRepository
{
    Task<PagedList<StudentProjection>> GetAllStudentsAsync(PaginationParams p, string? status = null);

}

