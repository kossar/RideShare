namespace API.DTO.v1.Models.Identity;

public class Login
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}