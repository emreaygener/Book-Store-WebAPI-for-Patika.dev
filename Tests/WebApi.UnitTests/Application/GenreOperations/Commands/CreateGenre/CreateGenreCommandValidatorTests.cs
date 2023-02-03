using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public CreateGenreCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
        }
        [Theory]
        [InlineData("")]
        [InlineData("Lr")]
        [InlineData("L")]
        [InlineData(" ")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string Name)
        {
            //Arrange
            CreateGenreCommand command = new(null);
            command.Model = new(){Name = Name};

            //Act
            CreateGenreCommandValidator validator=new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new(){Name = "Lord Of The Rings"};
            
            CreateGenreCommandValidator validator=new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }


    }
}