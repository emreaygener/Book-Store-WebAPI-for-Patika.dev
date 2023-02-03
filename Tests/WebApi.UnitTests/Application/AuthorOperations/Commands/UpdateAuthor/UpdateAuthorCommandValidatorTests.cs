using System;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
        }

        [Theory]
        [InlineData(0,"az","0",0,0)]
        [InlineData(-1,"azz","10",0,0)]
        [InlineData(-1,"bazz","10",1,1)]
        [InlineData(1,"zz","10",1,1)]
        [InlineData(1,"buzz",-1,1,1)]
        [InlineData(1,"buzz","10",-1,1)]
        [InlineData(1,"buzz","10",1,-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError(int id,string Name,string Surname,int GenreId,int AuthorId)
        {
            //Arrange
            UpdateAuthorCommand command = new(null);
            AuthorsViewModel model = new AuthorsViewModel(){Name=Name,Surname=Surname,DateOfBirth=DateTime.Now.AddYears(-10)};
            command.AuthorId = id;
            command.Model=model;

            //Act
            UpdateAuthorCommandValidator validator=new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}