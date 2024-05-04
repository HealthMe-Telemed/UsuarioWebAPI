using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UsuarioWebAPI.Controllers;
using UsuarioWebAPI.Models;
using UsuarioWebAPI.Services;
using Xunit;

namespace UsuarioWebAPI.Tests;

public class LoginTest
{
    [Fact]
    public async Task LoginInvalido()
    {
        var usuarioServiceMock = new Mock<IUsuarioService>();
        var tokenServiceMock = new Mock<ITokenService>();
        var loginForm = new LoginForm(){CPF="11111111111", Senha="12345"};
        usuarioServiceMock.Setup(s => s.Logar(loginForm)).ReturnsAsync((Usuario)null);
            var loggerMock = new Mock<ILogger<UsuarioController>>();
            var controller = new UsuarioController(loggerMock.Object, usuarioServiceMock.Object, tokenServiceMock.Object);

        var result = await controller.Login(loginForm);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);

    }

    [Fact]
    public async Task LoginValido()
    {
        var usuarioServiceMock = new Mock<IUsuarioService>();
        var tokenServiceMock = new Mock<ITokenService>();
        var loginForm = new LoginForm(){CPF="12345678900", Senha="senhaUsuario1"};
        var loggerMock = new Mock<ILogger<UsuarioController>>();
        var controller = new UsuarioController(loggerMock.Object, usuarioServiceMock.Object, tokenServiceMock.Object);
        
        var usuarioEsperado = new Usuario(){Id = 1, Ativo = true, CPF = "12345678900", DataNascimento = new DateTime(1999, 10, 14), Email = "usuario1@gmail.com", Nome = "Usuario1", Numero = "11111111111", Perfis = new List<Perfil>()};
        usuarioServiceMock.Setup(s => s.Logar(loginForm)).ReturnsAsync(usuarioEsperado);

        
        var perfis = new List<Perfil>() { new Perfil() {IdPerfil = 1, Descricao = "Paciente", IdMedico = 0, IdPaciente = 1}};
        usuarioEsperado.Perfis.AddRange(perfis);
        var usuarioPerfisEsperado =  usuarioEsperado;
    
        string tokenEsperado = "meu_token";

        usuarioServiceMock.Setup(s => s.BuscarPerfis(usuarioEsperado)).ReturnsAsync(usuarioPerfisEsperado);
        tokenServiceMock.Setup(s => s.GerarToken(usuarioPerfisEsperado)).Returns(tokenEsperado);
    

        var result = await controller.Login(loginForm);
            // Assert
            Assert.IsType<OkObjectResult>(result);


    }
}