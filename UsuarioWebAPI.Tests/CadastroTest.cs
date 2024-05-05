using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.Frameworks;
using UsuarioWebAPI.Controllers;
using UsuarioWebAPI.Models;
using UsuarioWebAPI.Services;
using Xunit;
using Xunit.Sdk;

namespace UsuarioWebAPI.Tests
{
    public class CadastroTest
    {
        [Fact]
        public async Task CadastroValido ()
        {
            
            //Arrange
            var usuarioServiceMock = new Mock<IUsuarioService>();
            var tokenServiceMock = new Mock<ITokenService>();
            var cadastroRequest = new CadastroRequest(){Nome = "Usuario1", Cpf = "12345678900", DataNascimento = new DateTime(1999, 01, 01), Email = "usuario1@gmail.com", Numero = "11999999999", Senha = "senhaUsuario1"};
            var loggerMock = new Mock<ILogger<UsuarioController>>();
            var controller = new UsuarioController(loggerMock.Object, usuarioServiceMock.Object, tokenServiceMock.Object);
            usuarioServiceMock.Setup(s => s.Cadastrar(cadastroRequest)).ReturnsAsync(true);

            //Act            
            var result = await controller.Cadastro(cadastroRequest);
            
            //Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]  
          public async Task CadastroInvalido ()
        {
            
            //Arrange
            var usuarioServiceMock = new Mock<IUsuarioService>();
            var tokenServiceMock = new Mock<ITokenService>();
            var cadastroRequest = new CadastroRequest(){Nome = "Usuario1", Cpf = "12345678900", DataNascimento = new DateTime(1999, 01, 01), Email = "usuario1@gmail.com", Numero = "11999999999", Senha = "senhaUsuario1"};
            var loggerMock = new Mock<ILogger<UsuarioController>>();
            var controller = new UsuarioController(loggerMock.Object, usuarioServiceMock.Object, tokenServiceMock.Object);
            usuarioServiceMock.Setup(s => s.Cadastrar(cadastroRequest)).ReturnsAsync(false);

            //Act
            var result = await controller.Cadastro(cadastroRequest);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }
    }
}