using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioWebAPI.Models;

namespace UsuarioWebAPI.Services
{
    public interface IUsuarioService
    {
        public Task<bool> Cadastrar(CadastroRequest request);
        public Task<bool> Logar(LoginForm login);

    }
}