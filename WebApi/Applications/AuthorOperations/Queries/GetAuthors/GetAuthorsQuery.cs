using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authorsList = _context.Authors.OrderBy(x => x.Id).ToList();

            return _mapper.Map<List<AuthorsViewModel>>(authorsList);
        }
    }
}