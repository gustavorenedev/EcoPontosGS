using AutoMapper;
using EcoPontos.Service.User.DTO;

namespace EcoPontos.Service.User.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, Domain.User.User>();
        CreateMap<Domain.User.User, RegisterUserDto>();
    }
}
