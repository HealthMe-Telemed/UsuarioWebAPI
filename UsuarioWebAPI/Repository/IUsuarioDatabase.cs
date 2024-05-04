using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioWebAPI.Models;

namespace UsuarioWebAPI.Repository
{
    public interface IUsuarioDatabase
    {
        public Task<Usuario> EncontrarUsuario(LoginForm login);
        public Task<bool> ValidarCadastro (CadastroRequest request);
        public Task <List<Perfil>> EncontrarPerfis(int usuarioId);
        public Task<int> EncontrarUsuarioCadastrado(string cpf);
        public Task AtualizarPerfis(int usuarioId);
    }
}