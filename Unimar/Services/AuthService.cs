using Unimar.Console.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Unimar.Console.Entities;

namespace Unimar.Console.Services
{
    public class AuthService : IAuth
    {
        private readonly Auth _opt;
        private readonly SigningCredentials _creds;
        private readonly string _user;
        private readonly string _aplication;
        
        public AuthService(IOptions<Auth> opt)
        {
            _opt = opt.Value;
            _user = _opt.User;
            _aplication = _opt.Aplication;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Token));
            _creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        public string GerarToken(IEnumerable<Claim> claims)
        {
            var claimsList = new List<Claim>(claims)
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: _opt.User,
                audience: _opt.Aplication,
                claims: claimsList,              
                expires: DateTime.Now.AddMinutes(_opt.AcessTokenMinutes),
                signingCredentials: _creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public Auth RetornaTokenValido()
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = GerarToken(claims);

            return new Auth
            {
                Token = token
            };

        }
    }
}
