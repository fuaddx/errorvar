using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.AuthsDtos;
using Twitter.Business.ExternalServices.Interfaces;
using Twitter.Core.Entities;

namespace Twitter.Business.ExternalServices.Implements
{
    public class TokenService : ITokenService
        
    {
        IConfiguration _configuration {  get; }

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto CreateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Surname, user.Surname));
            claims.Add(new Claim(ClaimTypes.GivenName, user.Name));
            claims.Add(new Claim("Test", user.BirthDay.ToString()));
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            DateTime expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetSection("Jwt")?["ExpireMin"]));
            JwtSecurityToken jwt = new JwtSecurityToken(
                _configuration.GetSection("Jwt")?["Issuer"],
                _configuration.GetSection("Jwt")?["Audience"],
                claims,
                DateTime.UtcNow,
               expires,
                cred
             );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new TokenDto
            {
                Expires = expires,
                Token = token
            };
        }
 
    }
}
