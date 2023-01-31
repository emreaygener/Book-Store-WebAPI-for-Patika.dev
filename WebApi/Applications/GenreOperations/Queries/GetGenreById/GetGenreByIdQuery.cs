using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using static WebApi.Common.ViewModels;

namespace WebApi.Applications.GenreOperations.Queries.GetGenreById
{
    public class GetGenreByIdQuery
    {
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetGenreByIdQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenresViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id==GenreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı!");

            return _mapper.Map<GenresViewModel>(genre);
        }
    }
}