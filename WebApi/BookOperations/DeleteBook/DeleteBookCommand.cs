using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
                throw new System.InvalidOperationException("Böyle bir kitap bulunamadı!");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}