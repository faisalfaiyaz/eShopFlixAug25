using AuthService.Application.DTOs;
using AuthService.Domain.Entities;
using AutoMapper;

namespace AuthService.Application.Mapper;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(r => r.Name).ToArray())).ReverseMap();

        CreateMap<SignUpDTO, User>().ReverseMap();
    }
}
