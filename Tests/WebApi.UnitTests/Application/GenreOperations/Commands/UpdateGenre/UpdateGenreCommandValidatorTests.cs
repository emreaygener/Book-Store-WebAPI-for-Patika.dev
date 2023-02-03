using System;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
        }

        [Theory]
        [InlineData(0,"az",true)]
        [InlineData(1,"az",true)]
        [InlineData(0,"azzzz",true)]
        [InlineData(-1,"zz",true)]
        [InlineData(-1,"zzzz",true)]
        [InlineData(0,"",true)]
        [InlineData(0,"string",true)]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError(int id,string Name,bool IsActive)
        {
            //Arrange
            UpdateGenreCommand command = new(null);
            UpdateGenreViewModel model = new UpdateGenreViewModel(){Name=Name,IsActive=true};
            command.GenreId = id;
            command.Model=model;

            //Act
            UpdateGenreCommandValidator validator=new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}