using EcoPontos.Service.Reward;
using EcoPontos.Service.Reward.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcoPontos.Controllers.Reward;

/// <summary>
/// Controller para gerenciar operações CRUD de Reward.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RewardController : ControllerBase
{
    private readonly IRewardService _rewardService;

    public RewardController(IRewardService rewardService)
    {
        _rewardService = rewardService;
    }

    /// <summary>
    /// Cria uma nova recompensa.
    /// </summary>
    /// <param name="dto">Dados da recompensa a serem criados.</param>
    /// <returns>Dados da recompensa criada.</returns>
    [HttpPost]
    public async Task<ActionResult<GetRewardDto>> CreateReward(CreateRewardDto dto)
    {
        var reward = await _rewardService.CreateRewardAsync(dto);
        return CreatedAtAction(nameof(GetRewardById), new { id = reward.RewardId }, reward);
    }

    /// <summary>
    /// Obtém todas as recompensas.
    /// </summary>
    /// <returns>Lista de recompensas.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetRewardDto>>> GetAllRewards()
    {
        var rewards = await _rewardService.GetAllRewardsAsync();
        return Ok(rewards);
    }

    /// <summary>
    /// Obtém uma recompensa pelo ID.
    /// </summary>
    /// <param name="id">ID da recompensa.</param>
    /// <returns>Dados da recompensa correspondente ao ID.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<GetRewardDto>> GetRewardById(int id)
    {
        var reward = await _rewardService.GetRewardByIdAsync(id);
        if (reward == null)
            return NotFound();

        return Ok(reward);
    }

    /// <summary>
    /// Atualiza uma recompensa existente.
    /// </summary>
    /// <param name="id">ID da recompensa a ser atualizada.</param>
    /// <param name="dto">Dados atualizados da recompensa.</param>
    /// <returns>Resultado da atualização.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReward(int id, UpdateRewardDto dto)
    {
        var success = await _rewardService.UpdateRewardAsync(id, dto);
        if (!success)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Exclui uma recompensa pelo ID.
    /// </summary>
    /// <param name="id">ID da recompensa a ser excluída.</param>
    /// <returns>Resultado da exclusão.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReward(int id)
    {
        var success = await _rewardService.DeleteRewardAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}