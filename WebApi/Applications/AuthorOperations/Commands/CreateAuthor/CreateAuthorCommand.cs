using System;
using System.Linq;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.CreateBook
{
    public class CreateAuthorCommand
    {
        public AuthorsViewModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public CreateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname && x.DateOfBirth == Model.DateOfBirth);
            
            if (author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut!");

            author = new Entities.Author();
            author.Name = Model.Name;
            author.Surname = Model.Surname;
            author.DateOfBirth = Model.DateOfBirth;

            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }
}