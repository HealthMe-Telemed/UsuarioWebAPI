using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioWebAPI.Models;

namespace UsuarioWebAPI.Services
{
    public interface ITokenService
    {
        public string GerarToken(Usuario usuario);
    }
}