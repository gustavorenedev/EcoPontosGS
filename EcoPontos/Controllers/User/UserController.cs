using EcoPontos.Service.User.DTO;
using EcoPontos.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace EcoPontos.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Registra um novo usuário no sistema.
    /// </summary>
    /// <param name="dto">Os detalhes do usuário para registro, incluindo nome, email, senha, etc.</param>
    /// <returns>Retorna uma resposta Created com o email do usuário registrado.</returns>
    [HttpPost("registerUser")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var result = await _userService.RegisterUser(dto);
        return Created($"/users/{result.Email}", result);
    }

    /// <summary>
    /// Realiza o login de um usuário existente.
    /// </summary>
    /// <param name="dto">Os detalhes de login, incluindo email e senha.</param>
    /// <returns>Retorna uma resposta Ok com o resultado do login do usuário.</returns>
    [HttpPost("loginUser")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _userService.Login(dto);
        return Ok(result);
    }

    /// <summary>
    /// Recupera um usuário pelo seu ID.
    /// </summary>
    /// <param name="userId">O ID do usuário a ser recuperado.</param>
    /// <returns>Retorna uma resposta Ok com os detalhes do usuário.</returns>
    [HttpPost("getUserById")]
    public async Task<IActionResult> GetUserById([FromQuery] int userId)
    {
        var result = await _userService.GetUserById(userId);
        return Ok(result);
    }
}
