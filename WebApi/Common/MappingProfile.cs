using System.Collections.Generic;
using AutoMapper;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.Entities;
using static WebApi.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            // CreateMap<List<BooksViewModel>,List<Book>>();  bir üst satırdaki GetBooks methodu için de karşılıyor .
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Author, AuthorsViewModel>();
        }
    }
}