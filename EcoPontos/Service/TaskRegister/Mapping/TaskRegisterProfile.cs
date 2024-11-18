using AutoMapper;
using EcoPontos.Service.TaskRegister.Dto;
using EcoPontos.Service.User.Dto;

namespace EcoPontos.Service.TaskRegister.Mapping;

public class TaskRegisterProfile : Profile
{
    public TaskRegisterProfile()
    {
        CreateMap<Domain.TaskRegister.TaskRegister, GetTasksDto>()
            .ForMember(dest => dest.TaskWorks, opt => opt.MapFrom(src => new List<Domain.TaskWork.TaskWork> { src.Task }))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<Domain.User.User, InfoUserReturn>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points));

        CreateMap<Domain.TaskRegister.TaskRegister, TaskAssignDto>();
    }
}
