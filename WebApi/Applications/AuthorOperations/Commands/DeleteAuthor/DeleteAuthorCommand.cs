using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteBook
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;


        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            var books = _context.Books.Where(x=>x.AuthorId==AuthorId).OrderBy(x=>x.Id).ToList();
            
            if (books.Count!=0)
                throw new InvalidOperationException("Yayında yazarın "+books.Count+" adet kitabı bulunmaktadır. Yazarların silinebilmesi için önce kitapları silinmelidir.");

            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı!");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}