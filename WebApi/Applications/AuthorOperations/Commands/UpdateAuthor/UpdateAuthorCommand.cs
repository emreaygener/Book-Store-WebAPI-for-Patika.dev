using System;
using System.Linq;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateBook
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public AuthorsViewModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id==AuthorId);

            if (author is null)
                throw new InvalidOperationException("Aradığınız yazar bulunamadı!");
            
            author.Name = Model.Name.Trim() == default || Model.Name.Trim() == "string" ? author.Name : Model.Name;
            author.Surname = Model.Surname.Trim() == default || Model.Surname.Trim() == "string" ? author.Surname : Model.Surname;;
            author.DateOfBirth = Model.DateOfBirth.Date != author.DateOfBirth.Date && Model.DateOfBirth.Date != DateTime.Now.Date ? Model.DateOfBirth : author.DateOfBirth;

            _context.SaveChanges();
        }
    }
}