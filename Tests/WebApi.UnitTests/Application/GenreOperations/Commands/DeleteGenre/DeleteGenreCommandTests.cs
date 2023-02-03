using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInputGivenRefersToAnEmpthyReference_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange
            DeleteGenreCommand command = new(_context);
            command.GenreId = _context.Genres.SingleOrDefault(x => x.Id == (_context.Genres.OrderByDescending(x => x.Id).First().Id)).Id + 1;

            //Act & Assertion
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Böyle bir kitap bulunamadı!");
        }


        [Fact]
        public void WhenErasedGenreIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            DeleteGenreCommand command = new(_context);
            command.GenreId = _context.Genres.SingleOrDefault(x => x.Id == (_context.Genres.OrderBy(x => x.Id).First().Id)).Id;
            command.Handle();

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Böyle bir kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            //Arrange
            DeleteGenreCommand command=new(_context);
            command.GenreId=_context.Genres.OrderBy(x=>x.Id).First().Id;
            //Act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            //Assert
            var Genre=_context.Genres.SingleOrDefault(Genre=>Genre.Id==command.GenreId);
            Genre.Should().BeNull();
        }
    }
}