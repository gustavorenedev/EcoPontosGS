using EcoPontos.Service.User.Dto;

namespace EcoPontos.Service.TaskRegister.Dto;

public class GetTasksDto
{
    public InfoUserReturn? User { get; set; }
    public IEnumerable<Domain.TaskWork.TaskWork>? TaskWorks { get; set; }
}
