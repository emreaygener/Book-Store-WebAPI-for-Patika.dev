using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _config;
        public string RefreshToken { get; set; }

        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                TokenHandler handler = new(_config);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate= token.ExpirationDate.AddMinutes(5);

                _context.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid bir refresh token bulunamadı!");
            }
        }
    }
}