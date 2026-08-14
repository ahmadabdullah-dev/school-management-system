namespace Business.Services;

public class StudentsService : IStudentsService
{
    private readonly IStudentsRepository _studentRepository;
    public StudentsService(IStudentsRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Result<PagedList<StudentDto>>> GetAllStudentsAsync(PaginationParams p, string? status = null)
    {
        var students = await _studentRepository.GetAllStudentsAsync(p, status);
        var dtos = new PagedList<StudentDto>()
        {
            Items = students.Items.Select(x => new StudentDto
            {
                StudentId = x.StudentId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                DateOfBirth = x.DateOfBirth,
                RegisteredAt = x.RegisteredAt,
                Status = x.Status
            }).ToList(),

            CurrentPage = students.CurrentPage,
            TotalCount = students.TotalCount,
            TotalPages = students.TotalPages,  
        };
        return Result<PagedList<StudentDto>>.Success(dtos);
    }
    public async Task<Result<int>> GetAllStudentsCountAsync(string? status = null)
    { 
        var studentsCount = await _studentRepository.GetAllStudentsCountAsync(status);
        return Result<int>.Success(studentsCount);
    }
    public async Task<Result<StudentDto>> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);

        if (student == null)
            return Result<StudentDto>.Failure("Student not found", 404);

        var dto = new StudentDto
        {
            StudentId = student.StudentId,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            DateOfBirth= student.DateOfBirth,
            Status = student.Status,
            RegisteredAt = student.RegisteredAt,
        };

        return Result<StudentDto>.Success(dto);
    }
<<<<<<< HEAD
    public async Task<Result<StudentDto>> GetStudentByEmailAsync(string email)
    {
        var student = await _studentRepository.GetStudentByEmailAsync(email);

        if (student == null)
            return Result<StudentDto>.Failure("Student not found", 404);

        var dto = new StudentDto
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

        return Result<StudentDto>.Success(dto);
    }

=======
>>>>>>> fcda06575dfb768219b1acdb38190f1438b8f530
}
