using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.UserOperations.Commands.CreateToken
{
    class CreateTokenCommand
    {
        public CreateTokenModel Model;
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x=>x.Email==Model.Email&&x.Password==Model.Password);
            if(user is not null)
            {
                TokenHandler handler = new(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate= token.ExpirationDate.AddMinutes(5);

                _context.SaveChanges();

                return token;
            }
            else
            {
                throw new System.InvalidOperationException("Kullanıcı adı veya şifre hatalı!");
            }
        }
    }
}