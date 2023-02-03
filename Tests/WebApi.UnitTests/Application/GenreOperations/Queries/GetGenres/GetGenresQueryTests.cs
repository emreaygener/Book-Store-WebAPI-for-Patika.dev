using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenExecuted_Genres_ShouldBeReturn()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);

            FluentActions
                        .Invoking(() => query.Handle())
                        .Should().NotThrow<System.Exception>();
        }
    }
}