using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TestsApp.Models.Users;

namespace TestsApp.Services
{
    public class TokenService
    {
        public TokenService()
        {
        }

        public string BuildToken(AppUser user, string configKey, string issuer)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.GetType().Name));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
