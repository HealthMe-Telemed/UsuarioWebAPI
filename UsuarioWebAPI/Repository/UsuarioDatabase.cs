using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using UsuarioWebAPI.Extensions;
using UsuarioWebAPI.Models;

namespace UsuarioWebAPI.Repository
{
    public class UsuarioDatabase : IUsuarioDatabase
    {
        private readonly MySqlConnection _database;
        private readonly ILogger<UsuarioDatabase> _logger;

        public UsuarioDatabase(MySqlConnection database, ILogger<UsuarioDatabase> logger)
        {
            _logger = logger;
            _database = database;
        }

        public async Task AtualizarPerfis(int usuarioId)
        {
            try
            {
                _logger.LogInformation("Tentando registrar o usuario no sistema...");
                
                await _database.ExecuteAsync("pInserirUsuarioPerfisNovo", 
                    new { 
                                usuarioId
                        }, commandType: CommandType.StoredProcedure);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro inesperado!! Segue o erro: {ex.Message}");
            }

        }

        public async Task<List<Perfil>> EncontrarPerfis(int usuarioId)
        {
            try
            {
                _logger.LogInformation($"Buscando perfis do usuario com id {usuarioId}");
                
                var perfis = await _database.QueryAsync<Perfil>(QueryExtensions.QueryBuscarPerfis(), 
                    new { id_usuario = usuarioId});
                return perfis.ToList();
            }

            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro inesperado!! Segue o erro: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado!!");
            }
        }

        public async Task<Usuario> EncontrarUsuario(LoginForm login)
        {
            try
            {
                _logger.LogInformation($"Buscando usuario com as credenciais CPF: {login.CPF} e senha: {login.Senha}...");
                
                var usuario = await _database.QueryFirstOrDefaultAsync<Usuario>(QueryExtensions.QueryConsultaCredenciais(), 
                    new { cpf = login.CPF, senha = login.Senha });
                return usuario;
            }

            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro inesperado!! Segue o erro: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado!!");
            }
        }

        public async Task<int> EncontrarUsuarioCadastrado(string cpf)
        {
            try
            {
                _logger.LogInformation($"Buscando usuario com CPF: {cpf} ...");
                
                var usuario = await _database.QueryFirstOrDefaultAsync<Usuario>(QueryExtensions.QueryConsultaUsuarioCadastrado(), 
                    new { cpf });
                return usuario.Id;
            }

            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro inesperado!! Segue o erro: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado!!");
            }
        }

        public async Task<Usuario> EncontrarUsuarioParaReset(ResetRequest resetRequest)
        {
            try
            {
                _logger.LogInformation($"Buscando usuario com CPF: {resetRequest.CPF} e email: {resetRequest.Email}...");
                
                var usuario = await _database.QueryFirstOrDefaultAsync<Usuario>(QueryExtensions.QueryConsultaUsuarioPraReset(), 
                    new { cpf = resetRequest.CPF, email = resetRequest.Email });
                return usuario;
            }

            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro inesperado!! Segue o erro: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado!!");
            }
        }

        public async Task<bool> ValidarCadastro(CadastroRequest request)
        {
            
            try
            {
                _logger.LogInformation("Tentando registrar o usuario no sistema...");
                
                var cadastroValido = await _database.ExecuteAsync("pInserirUsuarioeMedico", 
                    new { 
                            nomeUsuario = request.Nome,
                            cpfUsuario = request.Cpf,
                            numeroUsuario = request.Numero,
                            emailUsuario = request.Email,
                            dataNascimentoUsuario = request.DataNascimento,
                            senhaUsuario = request.Senha,
                            medico = request.Medico,
                            crmMedico = request.Crm,
                            idEspecialidade = request.IdEspecialidade
                        }, commandType: CommandType.StoredProcedure);
            }
            catch(MySqlException mySqlEx){
                _logger.LogError($"Ocorreu o seguinte erro ao cadastrar o usuario: {mySqlEx.SqlState} {mySqlEx.Message}");
                return false;
            }

            catch(Exception ex)
            {
                _logger.LogError($"Ocorreu um erro inesperado!! Segue o erro: {ex.Message}");
            }

            _logger.LogInformation("Usuário cadastrado no sistema");
            return true;
        }
    }
}