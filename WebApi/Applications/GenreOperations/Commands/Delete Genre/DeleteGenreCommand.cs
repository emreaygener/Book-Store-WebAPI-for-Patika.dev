using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            var books = _context.Books.Where(x => x.GenreId == GenreId).OrderBy(x => x.Id).ToList();

            if (books.Count != 0)
                throw new InvalidOperationException("Yayında bu türde " + books.Count + " adet kitap bulunmaktadır. Bu türü silebilmek için önce bu türdeki kitaplar silinmelidir.");

            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı!");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}