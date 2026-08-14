namespace Business.Interfaces;

public interface IStudentsService
{
    Task<Result<PagedList<StudentDto>>> GetAllStudentsAsync(PaginationParams p);
}
