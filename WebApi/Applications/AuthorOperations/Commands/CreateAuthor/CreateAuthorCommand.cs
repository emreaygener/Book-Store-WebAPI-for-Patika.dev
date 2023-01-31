using System;
using System.Linq;
using AutoMapper;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.AuthorOperations.Commands.CreateBook
{
    public class CreateAuthorCommand
    {
        public AuthorsViewModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname && x.DateOfBirth == Model.DateOfBirth);
            
            if (author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut!");

            author = _mapper.Map<Author>(Model);

            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }
}