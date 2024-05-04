using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioWebAPI.Repository;
using UsuarioWebAPI.Models;
using System.Text.RegularExpressions;

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

        public async Task<Usuario> Logar(LoginForm login)
        {
            var usuario = await _usuarioDatabase.EncontrarUsuario(login);

            return usuario;
        }

        public async Task<Usuario> BuscarPerfis(Usuario usuario)
        {
            var perfis = await _usuarioDatabase.EncontrarPerfis(usuario.Id);

            usuario.Perfis.AddRange(perfis);
            return usuario;

        }

        public async Task AtualizarPerfis(string cpf)
        {
            var usuarioId = await _usuarioDatabase.EncontrarUsuarioCadastrado(cpf);
            await _usuarioDatabase.AtualizarPerfis(usuarioId);
        }
    }
}