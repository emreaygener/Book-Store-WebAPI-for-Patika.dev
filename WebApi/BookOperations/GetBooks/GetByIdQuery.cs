using System;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }

        public GetByIdQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public BooksViewModel Handle()
        {
            var book = _dbContext.Books.Where(book=>book.Id==BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap mevcut değil!");
            BooksViewModel vm = _mapper.Map<BooksViewModel>(book);        //new();
            // vm.Title=book.Title;
            // vm.PageCount=book.PageCount;
            // vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy");
            // vm.Genre=((GenreEnum)book.GenreId).ToString();
            return vm;
        }
    }
}