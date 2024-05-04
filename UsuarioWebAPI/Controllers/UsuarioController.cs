using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
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
    private readonly ITokenService _tokenService;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService, ITokenService tokenService)
    {
        _logger = logger;
        _usuarioService = usuarioService;
        _tokenService = tokenService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginForm login)
    {
        var usuario = await _usuarioService.Logar(login);
        if (usuario is null)
        { 
            _logger.LogError("Usuário ou senha inválido");
            return Unauthorized("Usuário ou senha inválido");
        }
        
        _logger.LogInformation($@"Usuário encontrado com as seguintes informações: 
            Nome: {usuario.Nome}");

        var usuarioPerfis =  await _usuarioService.BuscarPerfis(usuario);   

        var token = _tokenService.GerarToken(usuarioPerfis);

        return Ok(new {
            Usuario = usuarioPerfis,
            Token = token
        });
    }

    [HttpPost]
    [Route("Cadastro")]
    public async Task<IActionResult> Cadastro([FromBody] CadastroRequest request)
    {
        
        var usuarioCadastrado = await _usuarioService.Cadastrar(request);
        
        if (!usuarioCadastrado) return BadRequest("Usuário já existente no sistema");

        await _usuarioService.AtualizarPerfis(request.Cpf);
        
        return Ok("Usuario Cadastrado com Sucesso");
    }

    
}
