using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using UsuarioWebAPI.Models;

namespace UsuarioWebAPI.Services
{
    public class TokenService : ITokenService
    {
        public string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario.Nome)
            };

            AdicionarRoles(claims, usuario.Perfis);

            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private void AdicionarRoles(List<Claim> claims, List<Perfil> perfis)
        {
            foreach (var perfil in perfis)
            {
                claims.Add(new Claim(ClaimTypes.Role, perfil.Descricao));
            }
        }
    }
}