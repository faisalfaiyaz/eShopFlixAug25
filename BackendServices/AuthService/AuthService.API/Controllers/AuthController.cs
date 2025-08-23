using AuthService.Application.DTOs;
using AuthService.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserAppService _userAppService;

    public AuthController(IUserAppService userAppService)
    {
        _userAppService = userAppService;
    }

    [HttpPost]
    public IActionResult Login(LoginDTO loginDTO)
    {
       UserDTO userDTO = _userAppService.LoginUser(loginDTO);
       if(userDTO == null)
       {
            return Unauthorized("Invalid login attempt.");
       }
        
       return Ok(userDTO);      
    }


    [HttpPost]
    public IActionResult SignUp(SignUpDTO signUpDTO, string role)
    {
        bool isSignedUp = _userAppService.SignUpUser(signUpDTO, role);
        if (!isSignedUp)
        {
            return BadRequest("Sign up failed. User may already exist.");
        }

        return Ok("User signed up successfully.");
    }
}
