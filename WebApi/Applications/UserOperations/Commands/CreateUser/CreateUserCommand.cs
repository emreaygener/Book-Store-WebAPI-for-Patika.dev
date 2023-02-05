using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.UserOperations.Commands.CreateUser
{
    class CreateUserCommand
    {
        public CreateUserModel Model;
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email);
            
            if (user is not null)
                throw new InvalidOperationException("Kullanıcı zaten mevcut.");
            
            user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}