using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration _configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            tokenModel.ExpirationDate = DateTime.Now.AddMinutes(15);

            JwtSecurityToken securityToken = new(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: tokenModel.ExpirationDate,
                notBefore: DateTime.Now,
                signingCredentials: credentials
            );

            JwtSecurityTokenHandler tokenHandler = new();

            //Token yaratılıyor.
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;
        }

        public string CreateRefreshToken(){return Guid.NewGuid().ToString();}
    }
}