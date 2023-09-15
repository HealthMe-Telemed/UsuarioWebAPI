using Microsoft.AspNetCore.Mvc;
using UsuarioWebAPI.Models;
using UsuarioWebAPI.Services;

namespace UsuarioWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
    {
        _logger = logger;
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginForm login)
    {
        var loginStatus = await _usuarioService.Logar(login);
        if (!loginStatus) return BadRequest("Usuário ou senha inválido");

        return Ok();
    }

    [HttpPost]
    [Route("Cadastro")]
    public async Task<IActionResult> Cadastro([FromBody] CadastroRequest request)
    {
        return Ok();
    }

    
}
