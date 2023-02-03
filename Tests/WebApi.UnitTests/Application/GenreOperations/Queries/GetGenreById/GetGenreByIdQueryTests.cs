using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Queries.GetGenreById;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.GenreOperations.Queries.GetGenreById
{
    public class GetGenreByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreByIdQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreByIdQuery query = new(_context,_mapper);
            query.GenreId=_context.Genres.OrderByDescending(x=>x.Id).First().Id+1;

            FluentActions
                .Invoking(()=>query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap mevcut değil!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //Arrange
            GetGenreByIdQuery query=new(_context,_mapper);
            GenresViewModel model = new();
            query.GenreId=_context.Genres.OrderBy(x=>x.Id).First().Id;
            //Act
            FluentActions.Invoking(()=>query.Handle()).Invoke();
            //Assert
            var Genre=_context.Genres.SingleOrDefault(Genre=>Genre.Id==query.GenreId);
            Genre.Should().NotBeNull();
        }
    }
}