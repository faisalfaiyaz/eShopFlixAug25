using AuthService.Application.DTOs;
using AuthService.Application.Repositories;
using AuthService.Application.Services.Abstractions;
using AuthService.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Application.Services.Implementations;

public class UserAppService : IUserAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserAppService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public IEnumerable<UserDTO> GetAllUsers()
    {
        IEnumerable<User> users = _userRepository.GetAll();
        if (users == null || !users.Any())
        {
            return Enumerable.Empty<UserDTO>();
        }

        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public UserDTO LoginUser(LoginDTO loginDTO)
    {
        if (loginDTO == null || string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
        {
            throw new ArgumentException("Invalid login data");
        }

        User user = _userRepository.GetUserByEmail(loginDTO.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        // Map User entity to UserDTO
        UserDTO userDTO = _mapper.Map<UserDTO>(user);

        // Generate JWT token
        userDTO.Token = GenerateJwtToken(userDTO);

        return userDTO;
    }

    public bool SignUpUser(SignUpDTO signUpDTO, string role)
    {

        if (signUpDTO == null)
        {
            throw new ArgumentException("Invalid SignUp Data");
        }

        if (string.IsNullOrEmpty(role))
        {
            throw new ArgumentException("Role must be provided");
        }

        // Check if user with the same email already exists
        var existingUser = _userRepository.GetUserByEmail(signUpDTO.Email);
        if (existingUser != null)
        {
            throw new ArgumentException("Email is already in use");
        }

        // Map SignUpDTO to User entity
        User newUser = _mapper.Map<User>(signUpDTO);

        // Hash the password before saving
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(signUpDTO.Password);

        return _userRepository.RegisterUser(newUser, role);
    }

    private string GenerateJwtToken(UserDTO user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        int ExpireMinutes = Convert.ToInt32(_configuration["Jwt:ExpireMinutes"]);

        var claims = new[] {
                             new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                             new Claim(JwtRegisteredClaimNames.Email, user.Email),
                             new Claim("Roles", string.Join(",",user.Roles)),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                             };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                        _configuration["Jwt:Audience"],
                                        claims,
                                        expires: DateTime.UtcNow.AddMinutes(ExpireMinutes), //token expiry minutes
                                        signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
