using Microsoft.AspNetCore.Mvc;
using UsuarioWebAPI.Models;

namespace UsuarioWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginForm login)
    {
        return Ok("Login com sucesso");
    }

    [HttpPost]
    [Route("Cadastro")]
    public async Task<IActionResult> Cadastro([FromBody] CadastroRequest request)
    {
        return Ok();
    }

    
}
