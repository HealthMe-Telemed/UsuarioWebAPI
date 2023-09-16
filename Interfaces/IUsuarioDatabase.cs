using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioWebAPI.Models;

namespace UsuarioWebAPI.Interfaces
{
    public interface IUsuarioDatabase
    {
        public Task<bool> EncontrarUsuario(LoginForm login);
        public Task<bool> ValidarCadastro (CadastroRequest request);
    }
}