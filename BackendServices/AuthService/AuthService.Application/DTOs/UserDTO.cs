namespace AuthService.Application.DTOs;

public class UserDTO
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string[] Roles { get; set; }
    public string Token { get; set; }
}
