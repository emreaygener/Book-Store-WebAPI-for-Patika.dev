using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;

        public DeleteAuthorCommandValidatorTests(CommonTestFixture testFixture)
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
            DeleteAuthorCommand command = new(null);
            command.AuthorId = id;

            //Act
            DeleteAuthorCommandValidator validator=new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnErrors()
        {
            DeleteAuthorCommand command = new(_context);
            command.AuthorId = _context.Authors.OrderBy(x => x.Id).First().Id;

            DeleteAuthorCommandValidator validator=new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}