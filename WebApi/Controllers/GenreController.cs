using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.Applications.GenreOperations.Queries.GetGenreById;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using static WebApi.Common.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]

    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new(_context, _mapper);
            return Ok(query.Handle());
        }

        [HttpGet("id")]
        public IActionResult GetGenreById(int id)
        {
            GetGenreByIdQuery query = new(_context, _mapper);
            query.GenreId = id;

            GetGenreByIdQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult CreateGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new(_context);

            command.Model = newGenre;

            CreateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreViewModel updatedGenre)
        {

            UpdateGenreCommand command = new(_context);

            command.GenreId = id;
            command.Model = updatedGenre;

            UpdateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {

            DeleteGenreCommand command = new(_context);

            command.GenreId = id;

            DeleteGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}