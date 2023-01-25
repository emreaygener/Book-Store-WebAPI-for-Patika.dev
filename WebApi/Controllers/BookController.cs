using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BooksViewModel result;
            try
            {
                GetByIdQuery query = new GetByIdQuery(_context,_mapper);
                query.BookId = id; //* => bu şekilde id injection yapılabilir
                
                GetByIdQueryValidator validator = new();
                validator.ValidateAndThrow(query);

                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new(_context,_mapper);
            try
            {
                command.Model = newBook;
                
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new(_context);
                command.BookId=id;
                command.Model = updatedBook;

                UpdateBookCommandValidator validator = new();
                validator.ValidateAndThrow(command);
                
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new(_context);
                command.BookId=id;

                DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);

                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
    }
}



// private static List<Book>BookList = new List<Book>()
// {
//     new Book{
//         Id=1,
//         Title="Lean Startup",
//         GenreId=1, //Personal Growth
//         PageCount = 200,
//         PublishDate = new DateTime(2001,06,12)
//     },
//     new Book{
//         Id=2,
//         Title="Herland",
//         GenreId=2, //Sci-fi
//         PageCount = 250,
//         PublishDate = new DateTime(2010,05,23)
//     },
//     new Book{
//         Id=3,
//         Title="Dune",
//         GenreId=2, //Sci-fi
//         PageCount = 540,
//         PublishDate = new DateTime(2001,12,21)
//     }
// };

// [HttpGet]
// public Book Get([FromQuery] string id)
// {
//     var book = _context.Books.Where(book=>book.Id==Convert.ToInt32(id)).SingleOrDefault();
//     return book;
// }

/*
[HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new(_context,_mapper);
            try
            {
                command.Model = newBook;
                
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                //! if(validator.Validate(command).IsValid)         => Bu satıra ihtiyaç yok çünkü throw ile hata fırlar fırlamaz catch yakalıyor böylelikle hata çıkarsa command.handle() atlanmış oluyor!
                    command.Handle();
                //* ValidationResult result = validator.Validate(command);
                //* if(!result.IsValid)
                //*     foreach (var item in result.Errors)
                //*         Console.WriteLine("Özellik: " + item.PropertyName + " - Error Message: " + item.ErrorMessage);
                //* else
                //*     command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
    */
    