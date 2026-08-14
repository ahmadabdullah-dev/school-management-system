namespace Business.Interfaces;

public interface IStudentsService
{
    Task<Result<PagedList<StudentDto>>> GetAllStudentsAsync(PaginationParams p, string? status = null);
    Task<Result<int>> GetAllStudentsCountAsync(string? status = null);
    Task<Result<StudentDto>> GetStudentByIdAsync(int id);
<<<<<<< HEAD
    Task<Result<StudentDto>> GetStudentByEmailAsync(string email);

=======
>>>>>>> fcda06575dfb768219b1acdb38190f1438b8f530
}
