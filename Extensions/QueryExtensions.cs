using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuarioWebAPI.Extensions
{
    public static class QueryExtensions
    {
        public static string QueryConsultaCredenciais() => @"
        SELECT 
            U.id as 'Id', 
            U.nome as 'Nome', 
            U.cpf as 'CPF', 
            U.numero as 'Numero', 
            U.data_nascimento as 'DataNascimento', 
            U.ativo as 'Ativo' FROM usuario U 
            WHERE U.cpf = @cpf AND U.senha = @senha;";
    }
}