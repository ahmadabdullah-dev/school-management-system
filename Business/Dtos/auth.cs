namespace Business.Dtos;

public class LoginDto
{
    public required string email { get; set; }
    public required string password { get; set; }
    public required bool isPersistence { get; set; }
}
