namespace Business.Interfaces;

public interface IStudentsService
{
    Task<Result<PagedList<StudentDto>>> GetAllStudentsAsync(PaginationParams p, string? status = null);
    Task<Result<int>> GetAllStudentsCountAsync(string? status = null);

}
