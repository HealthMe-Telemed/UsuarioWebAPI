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

        public static string QueryBuscarPerfis() => @"
        SELECT p.id as 'IdPerfil', p.descricao as 'Descricao' FROM perfil p
            INNER JOIN alocacao_usuario_perfil aup
            ON aup.id_perfil = p.id
            INNER JOIN usuario u
            ON aup.id_usuario = u.id
            WHERE u.id = @id_usuario;";
    }
    
}