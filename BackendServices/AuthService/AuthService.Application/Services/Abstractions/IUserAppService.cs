﻿using AuthService.Application.DTOs;

namespace AuthService.Application.Services.Abstractions;

public interface IUserAppService
{
    UserDTO LoginUser(LoginDTO loginDTO);
    bool SignUpUser(SignUpDTO signUpDTO, string role);
    IEnumerable<UserDTO> GetAllUsers();

}
