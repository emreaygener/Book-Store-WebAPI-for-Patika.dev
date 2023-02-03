using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;

        public DeleteGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            _mapper=testFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError(int id)
        {
            //Arrange
            DeleteGenreCommand command = new(null);
            command.GenreId = id;

            //Act
            DeleteGenreCommandValidator validator=new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnErrors()
        {
            DeleteGenreCommand command = new(_context);
            command.GenreId = _context.Genres.OrderBy(x => x.Id).First().Id;

            DeleteGenreCommandValidator validator=new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}