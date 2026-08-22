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
    public async Task<Result<string>> AddStudentAsync(AddStudentDto dto)
    {
        var studentEntity = new Student
        {
            FirstName= dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            RegisteredAt = DateTime.UtcNow,
            Status = StudentStatuses.ACTIVE,
        };

        if (await _studentRepository.IsEmailExists(dto.Email))
            return Result<string>.Failure("Email already exists",400);

        var addResult = await _studentRepository.AddStudentAsync(studentEntity);
       
        return addResult != null 
            ? Result<string>.Success("Student added successfully") 
            : Result<string>.Failure("Unexpected error happened", 404); 
    }
    public async Task<Result<string>> UpdateStudentAsync(UpdateStudentDto dto)
    {
        var entity = await _studentRepository.GetStudentEntityByIdAsync(dto.StudentId);
        
        if (entity == null)
            return Result<string>.Failure("Student not found", 404);
      
        if(!string.IsNullOrEmpty(dto.FirstName))
            entity.FirstName = dto.FirstName;
        
        if(!string.IsNullOrEmpty(dto.LastName))
            entity.LastName = dto.LastName;

        if (!string.IsNullOrEmpty(dto.Email) && dto.Email != entity.Email)
        {
            if (await _studentRepository.IsEmailExists(dto.Email))
                return Result<string>.Failure("Email already taken", 400);
            
            entity.Email = dto.Email;
        }

        if (!string.IsNullOrEmpty(dto.PhoneNumber))
            entity.PhoneNumber = dto.PhoneNumber;
       
        if((dto.DateOfBirth.HasValue))
            entity.DateOfBirth = dto.DateOfBirth.Value;

        if (!string.IsNullOrEmpty(dto.Status))
            entity.Status = dto.Status;

        var isUpdated = await _studentRepository.UpdateStudentAsync(entity);

        if (!isUpdated)
            return Result<string>.Failure("Unexpected errror happened", 400);

        return Result<string>.Success("Student updated successfully");
    }
 
}
