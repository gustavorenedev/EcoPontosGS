using EcoPontos.Service.TaskWork;
using EcoPontos.Service.TaskWork.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoPontos.Controllers.Task;

[Route("api/[controller]")]
[ApiController]
public class TaskWorkController : ControllerBase
{
    private readonly ITaskWorkService _taskWorkService;

    public TaskWorkController(ITaskWorkService taskWorkService)
    {
        _taskWorkService = taskWorkService;
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <param name="dto">Os detalhes da tarefa, incluindo tipo e descrição.</param>
    /// <returns>Retorna uma resposta Ok com os detalhes da tarefa criada.</returns>
    [HttpPost("CreateTaskWork")]
    public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskWorkDto dto)
    {
        var result = await _taskWorkService.CreateTaskAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// Recupera todas as tarefas.
    /// </summary>
    /// <returns>Retorna uma resposta Ok com uma lista de todas as tarefas.</returns>
    [HttpGet("GetTaskWorkAll")]
    public async Task<IActionResult> GetTaskAll()
    {
        var result = await _taskWorkService.GetTaskAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Recupera uma tarefa pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da tarefa a ser recuperada.</param>
    /// <returns>Retorna uma resposta Ok com os detalhes da tarefa.</returns>
    [HttpGet("User/{id}")]
    public async Task<IActionResult> GetTaskByIdAsync(int id)
    {
        var result = await _taskWorkService.GetTaskByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Exclui uma tarefa pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da tarefa a ser excluída.</param>
    /// <returns>Retorna uma resposta Ok se a tarefa foi excluída com sucesso.</returns>
    [HttpDelete("DeleteTaskWork/{id}")]
    public async Task<IActionResult> DeleteTaskAsync(int id)
    {
        var result = await _taskWorkService.DeleteTaskAsync(id);
        return Ok(result);
    }
}
