using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.Common;
using WebApi.DBOperations;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.BookOperations.Queries.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }

        public GetBookByIdQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public BooksViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).Where(book => book.Id == BookId).SingleOrDefault();
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