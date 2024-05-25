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
            U.email as 'Email',
            U.data_nascimento as 'DataNascimento', 
            U.ativo as 'Ativo',
            M.crm AS 'CRM',
            E.descricao AS 'Especialidade'
            FROM usuario U
			Left JOIN medico M ON M.usuario_id = U.id
            LEFT JOIN alocacao_especialidade AE ON AE.medico_id = M.id
			LEFT JOIN especialidade E ON E.id = AE.espec_id  
            WHERE U.cpf = @cpf AND U.senha = @senha AND U.ativo = 1;";

        public static string QueryBuscarPerfis() => @"
        SELECT p.id as 'IdPerfil', p.descricao as 'Descricao', vp.paciente_id AS 'IdPaciente', vp.medico_id AS 'IdMedico' FROM healthme.perfil p
            INNER JOIN healthme.alocacao_usuario_perfil aup
            ON aup.id_perfil = p.id
            INNER JOIN healthme.usuario u
            ON aup.id_usuario = u.id
            INNER JOIN healthme.v_perfis vp
            ON vp.usuario_id = u.id
            WHERE u.id = @id_usuario;";

        public static string QueryConsultaUsuarioCadastrado() => @"SELECT 
            U.id as 'Id', 
            U.nome as 'Nome', 
            U.cpf as 'CPF', 
            U.numero as 'Numero',
            U.email as 'Email',
            U.data_nascimento as 'DataNascimento', 
            U.ativo as 'Ativo' FROM usuario U 
            WHERE U.cpf = @cpf;";

        public static string QueryConsultaUsuarioPraReset() =>
            @"
        SELECT 
            U.id as 'Id', 
            U.nome as 'Nome', 
            U.cpf as 'CPF', 
            U.numero as 'Numero',
            U.email as 'Email',
            U.data_nascimento as 'DataNascimento', 
            U.ativo as 'Ativo' FROM usuario U 
            WHERE U.cpf = @cpf AND U.email = @email;";

        public static string InserirToken() => @"
        INSERT INTO tokenReset(id_usuario, token, data_expiracao)
        VALUES(@id_usuario, @token, @data_expiracao);";

        public static string QueryConsultaToken() =>
            @"
        SELECT 
            T.token FROM tokenReset T 
            WHERE T.data_expiracao > @data_atual AND T.token = @token;";

        public static string QueryConsultaTokenCpf() =>
            @"
        SELECT 
            T.token FROM tokenReset T 
            INNER JOIN usuario U
            ON T.id_usuario = U.id
            WHERE T.data_expiracao > @data_atual AND T.token = @token AND U.cpf = @cpf;";

        public static string AlterarSenha() => @"
        UPDATE usuario SET senha = @novaSenha
        WHERE usuario.cpf = @cpf;";

        public static string ExcluirUsuario() => @"
        UPDATE usuario SET ativo = 0 WHERE id = @id;
        UPDATE medico SET ativo = 0 WHERE usuario_id = @id;
        UPDATE paciente SET ativo = 0 WHERE usuario_id = @id;
        UPDATE agendamento INNER JOIN medico ON agendamento.medico_id = medico.id
        INNER JOIN paciente ON agendamento.paciente_id = paciente.id 
        SET agendamento.status_id = 3
        WHERE paciente.usuario_id = @id OR medico.usuario_id = @id;";
    }
    
}