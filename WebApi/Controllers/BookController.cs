using System;
using System.Collections.Generic;
using System.Linq;
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
        public BookController(BookStoreDbContext context)
        {
            _context=context;            
        }
        
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result); 
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdQuery query = new GetByIdQuery(_context);
            try
            {
            var result = query.Handle(id);
            return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody]CreateBookModel newBook)
        {
            CreateBookCommand command = new(_context);
            try
            {
            command.Model=newBook;
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
        public IActionResult UpdateBook(int id,[FromBody]UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new(_context);
            try
            {
                command.Model = updatedBook;
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook (int id)
        {
           DeleteBookCommand command = new(_context);
           try
           {
            command.Handle(id);
            return Ok();
           }
           catch (Exception ex)
           {
            return BadRequest(ex.Message);
           }
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