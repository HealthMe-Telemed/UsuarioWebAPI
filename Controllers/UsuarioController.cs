using System.Security.Cryptography;
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
        var usuario = await _usuarioService.Logar(login);
        if (usuario is null)
        { 
            _logger.LogError("Usuário ou senha inválido");
            return BadRequest("Usuário ou senha inválido");
        }
        
        _logger.LogInformation($@"Usuário encontrado com as seguintes informações: 
            Nome: {usuario.Nome}, CPF: {usuario.CPF}, Numero: {usuario.Numero}, Data de Nasimento: {usuario.DataNascimento.ToString("yyyy-MM-dd")}");
        return Ok(usuario);
    }

    [HttpPost]
    [Route("Cadastro")]
    public async Task<IActionResult> Cadastro([FromBody] CadastroRequest request)
    {
        
        var usuarioCadastrado = await _usuarioService.Cadastrar(request);
        
        if (!usuarioCadastrado) return BadRequest("Usuário já existente no sistema");
        
        return Ok("Usuario Cadastrado com Sucesso");
    }

    
}
