namespace Business.Services;

public class StudentsService : IStudentsService
{
    private readonly IStudentRepository _studentRepository;
    public StudentsService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Result<PagedList<StudentDto>>> GetAllStudentsAsync(PaginationParams p)
    {
        var students = await _studentRepository.GetAllStudentsAsync(p);
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
}
