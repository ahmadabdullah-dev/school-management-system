namespace DataAccess.Interfaces;

public interface IStudentsRepository
{
    Task<PagedList<StudentProjection>> GetAllStudentsAsync(PaginationParams p, string? status = null);
    Task<int> GetAllStudentsCountAsync(string? status = null);
    Task<StudentProjection?> GetStudentByIdAsync(int id);
    Task<StudentProjection?> GetStudentByEmailAsync(string email);
    Task<bool> IsEmailExists(string email);
    Task<Student?> GetStudentEntityByIdAsync(int id);
    Task<int?> AddStudentAsync(Student student);
    Task<bool> UpdateStudentAsync(Student student);
}

