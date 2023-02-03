using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings","0",0,0)]
        [InlineData("Lord Of The Rings","0",1,1)]
        [InlineData("Lord Of The Rings","100",0,0)]
        [InlineData("","0",0,0)]
        [InlineData("","100",1,1)]
        [InlineData("","0",1,1)]
        [InlineData("Lor","100",1,1)]
        [InlineData("Lord","100",0,0)]
        [InlineData("Lord","0",1,1)]
        [InlineData("Lord","10",0,1)]
        [InlineData("Lord","10",1,0)]
        [InlineData(" ","100",1,1)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string Name,string Surname, int genreId, int authorId)
        {
            //Arrange
            CreateAuthorCommand command = new(null, null);
            command.Model = new()
            {
                Name = Name,
                Surname = Surname,
                DateOfBirth = DateTime.Now.Date.AddYears(-1),
            };

            //Act
            CreateAuthorCommandValidator validator=new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new(null, null);
            command.Model = new()
            {
                Name = "Lord Of The Rings",
                Surname = "100",
                DateOfBirth = DateTime.Now.Date,
            };
            
            CreateAuthorCommandValidator validator=new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new(null, null);
            command.Model = new()
            {
                Name = "Lord Of The Rings",
                Surname = "100",
                DateOfBirth = DateTime.Now.Date.AddYears(-2),
            };
            
            CreateAuthorCommandValidator validator=new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }


    }
}