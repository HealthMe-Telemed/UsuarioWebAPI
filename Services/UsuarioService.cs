using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioWebAPI.Interfaces;
using UsuarioWebAPI.Models;

namespace UsuarioWebAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ILogger<UsuarioService> _logger;
        private readonly IUsuarioDatabase _usuarioDatabase;
        public UsuarioService(ILogger<UsuarioService> logger, IUsuarioDatabase usuarioDatabase)
        {
            _logger = logger;
            _usuarioDatabase = usuarioDatabase;
        }
        public async Task<bool> Cadastrar(CadastroRequest request)
        {
            var cadastroValido = await _usuarioDatabase.ValidarCadastro(request);
            return cadastroValido;
        }

        public async Task<bool> Logar(LoginForm login)
        {
            var loginStatus = await _usuarioDatabase.EncontrarUsuario(login);
            return loginStatus;
        }
    }
}