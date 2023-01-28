using System;
using System.Linq;
using AutoMapper;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorByIdQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorsViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar zaten mevcut!");
                
            return _mapper.Map<AuthorsViewModel>(author);
        }
    }
}