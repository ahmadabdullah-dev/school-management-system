namespace DataAccess.Interfaces;

public interface IStudentRepository
{
    Task<PagedList<StudentProjection>> GetAllStudentsAsync(PaginationParams p);
}
