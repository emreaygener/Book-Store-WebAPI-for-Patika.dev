using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Applications.UserOperations.Commands.CreateToken;
using WebApi.Applications.UserOperations.Commands.CreateUser;
using WebApi.Applications.UserOperations.Commands.RefreshToken;
using WebApi.Applications.UserOperations.Queries.GetUsers;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;
using static WebApi.Common.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;

        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new(_context,_mapper);
            command.Model = newUser;
            
            CreateUserCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody]CreateTokenModel loginModel)
        {
            CreateTokenCommand command = new(_context,_configuration);
            command.Model = loginModel;
            var token = command.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery]string token)
        {
            RefreshTokenCommand command = new(_context,_configuration);
            command.RefreshToken = token;
            var resultToken = command.Handle();
            return resultToken;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            GetUsersQuery query = new(_context,_mapper);
            return Ok(query.Handle());
        }
    }
}