using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings",0,0,0)]
        [InlineData("Lord Of The Rings",0,1,1)]
        [InlineData("Lord Of The Rings",100,0,0)]
        [InlineData("",0,0,0)]
        [InlineData("",100,1,1)]
        [InlineData("",0,1,1)]
        [InlineData("Lor",100,1,1)]
        [InlineData("Lord",100,0,0)]
        [InlineData("Lord",0,1,1)]
        [InlineData("Lord",10,0,1)]
        [InlineData("Lord",10,1,0)]
        [InlineData(" ",100,1,1)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string title,int pageCount, int genreId, int authorId)
        {
            //Arrange
            CreateBookCommand command = new(null, null);
            command.Model = new()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId,
                AuthorId = authorId
            };

            //Act
            CreateBookCommandValidator validator=new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new(null, null);
            command.Model = new()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1,
                AuthorId = 1
            };
            
            CreateBookCommandValidator validator=new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new(null, null);
            command.Model = new()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1,
                AuthorId = 1
            };
            
            CreateBookCommandValidator validator=new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }


    }
}