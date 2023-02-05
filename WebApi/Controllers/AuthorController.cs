using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.AuthorOperations.Commands.CreateBook;
using WebApi.Applications.AuthorOperations.Commands.DeleteBook;
using WebApi.Applications.AuthorOperations.Commands.UpdateBook;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorById;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult GetAuthorById(int id)
        {
            GetAuthorByIdQuery query = new(_context, _mapper);
            query.AuthorId = id;

            GetAuthorByIdQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorsViewModel newAuthor)
        {
            CreateAuthorCommand command = new(_context,_mapper);

            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
        
        [HttpPut("id")]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorsViewModel updatedAuthor)
        {

            UpdateAuthorCommand command = new(_context);

            command.AuthorId = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteAuthor(int id)
        {

            DeleteAuthorCommand command = new(_context);

            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}