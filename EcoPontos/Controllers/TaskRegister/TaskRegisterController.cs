using EcoPontos.Service.TaskRegister;
using Microsoft.AspNetCore.Mvc;

namespace EcoPontos.Controllers.TaskRegister;

[Route("api/[controller]")]
[ApiController]
public class TaskRegisterController : ControllerBase
{
    private readonly ITaskRegisterService _taskRegisterService;

    public TaskRegisterController(ITaskRegisterService taskRegisterService)
    {
        _taskRegisterService = taskRegisterService;
    }

    /// <summary>
    /// Conclui uma tarefa para um usuário com uma duração especificada.
    /// </summary>
    /// <param name="userId">O ID do usuário que concluiu a tarefa.</param>
    /// <param name="taskId">O ID da tarefa a ser concluída.</param>
    /// <param name="duration">A duração gasta na tarefa.</param>
    /// <returns>Retorna uma resposta Ok com os detalhes da conclusão ou NotFound se a tarefa não foi encontrada.</returns>
    [HttpPost("CompleteTask")]
    public async Task<IActionResult> CompleteTaskAsync([FromQuery] int userId, [FromQuery] int taskId, [FromQuery] int duration)
    {
        var result = await _taskRegisterService.CompleteTaskAsync(userId, taskId, duration);

        if (result == 0)
        {
            return NotFound("Task not found or could not be completed.");
        }

        return Ok(result);
    }

    /// <summary>
    /// Atribui uma tarefa a um usuário.
    /// </summary>
    /// <param name="userId">O ID do usuário a quem a tarefa será atribuída.</param>
    /// <param name="taskId">O ID da tarefa a ser atribuída.</param>
    /// <returns>Retorna uma resposta Ok indicando o resultado da atribuição da tarefa.</returns>
    [HttpPost("AssignTaskToUser")]
    public async Task<IActionResult> AssignTaskToUserAsync([FromQuery] int userId, [FromQuery] int taskId)
    {
        var result = await _taskRegisterService.AssignTaskToUserAsync(userId, taskId);
        return Ok(result);
    }

    /// <summary>
    /// Desatribui uma tarefa de um usuário.
    /// </summary>
    /// <param name="userId">O ID do usuário de quem a tarefa será desatribuída.</param>
    /// <param name="taskId">O ID da tarefa a ser desatribuída.</param>
    /// <returns>Retorna uma resposta Ok se a tarefa foi desatribuída com sucesso ou NotFound se a associação da tarefa não foi encontrada.</returns>
    [HttpPost("UnassignTaskFromUser")]
    public async Task<IActionResult> UnassignTaskFromUserAsync([FromQuery] int userId, [FromQuery] int taskId)
    {
        var result = await _taskRegisterService.UnassignTaskFromUserAsync(userId, taskId);
        return result ? Ok("Task unassigned successfully.") : NotFound("Task association not found.");
    }

    /// <summary>
    /// Recupera todas as tarefas associadas a um usuário especificado.
    /// </summary>
    /// <param name="userId">O ID do usuário cujas tarefas serão recuperadas.</param>
    /// <returns>Retorna uma resposta Ok com uma lista de tarefas para o usuário ou NotFound se nenhuma tarefa estiver associada ao usuário.</returns>
    [HttpGet("GetTasksByUser/{userId}")]
    public async Task<IActionResult> GetTasksByUserAsync(int userId)
    {
        var tasks = await _taskRegisterService.GetTasksByUserIdAsync(userId);

        if (tasks == null || !tasks.Any())
        {
            return NotFound("No tasks associated with the user.");
        }

        return Ok(tasks);
    }
}