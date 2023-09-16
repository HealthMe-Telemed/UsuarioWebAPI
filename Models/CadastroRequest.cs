using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuarioWebAPI.Models
{
    public class CadastroRequest
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Numero { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Senha { get; set; }
    }
}