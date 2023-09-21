using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using UsuarioWebAPI.Models;

namespace UsuarioWebAPI.Interfaces
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
        
        public async Task<bool> EncontrarUsuario(LoginForm login)
        {
            return true;
        }

        public async Task<bool> ValidarCadastro(CadastroRequest request)
        {
            
            try
            {
                _logger.LogInformation("Tentando registrar o usuario no sistema...");
                
                var cadastroValido = await _database.ExecuteAsync("pInserirUsuario", 
                    new { 
                            nomeUsuario = request.Nome,
                            cpfUsuario = request.Cpf,
                            numeroUsuario = request.Numero,
                            dataNascimentoUsuario = request.DataNascimento,
                            senhaUsuario = request.Senha
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