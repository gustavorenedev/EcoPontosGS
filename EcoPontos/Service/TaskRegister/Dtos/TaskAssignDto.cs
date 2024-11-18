using EcoPontos.Service.User.Dto;

namespace EcoPontos.Service.TaskRegister.Dto;

public class TaskAssignDto
{
    public InfoUserReturn? User { get; set; }
    public Domain.TaskWork.TaskWork? Task { get; set; }
}
