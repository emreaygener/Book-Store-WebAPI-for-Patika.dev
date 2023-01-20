using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetByIdQuery(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public BooksViewModel Handle(int id)
        {
            var book = _dbContext.Books.Where(book=>book.Id==id).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap mevcut değil!");
            BooksViewModel vm = new();
            vm.Title=book.Title;
            vm.PageCount=book.PageCount;
            vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.Genre=((GenreEnum)book.GenreId).ToString();
            return vm;
        }
    }
}