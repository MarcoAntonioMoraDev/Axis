using CooperativaApp.Domain.Entities;
using CooperativaApp.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CooperativaApp.InfraSecurity.Services
{    
    public class UsuarioSecurity : IUsuarioSecurity
    {
        private readonly IConfiguration _configuration;

        public UsuarioSecurity(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Usuario usuario)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
