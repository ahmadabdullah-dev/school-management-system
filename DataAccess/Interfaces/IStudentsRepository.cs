namespace DataAccess.Interfaces;

public interface IStudentsRepository
{
    Task<PagedList<StudentProjection>> GetAllStudentsAsync(PaginationParams p, string? status = null);
    Task<int> GetAllStudentsCountAsync(string? status = null);
    Task<StudentProjection?> GetStudentByIdAsync(int id);
<<<<<<< HEAD
    Task<StudentProjection?> GetStudentByEmailAsync(string email);

=======
>>>>>>> fcda06575dfb768219b1acdb38190f1438b8f530
}

