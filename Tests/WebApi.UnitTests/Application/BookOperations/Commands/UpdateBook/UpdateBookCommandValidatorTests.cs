using System;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
        }

        [Theory]
        [InlineData(0,"az",0,0,0)]
        [InlineData(-1,"azz",10,0,0)]
        [InlineData(-1,"bazz",10,1,1)]
        [InlineData(1,"zz",10,1,1)]
        [InlineData(1,"buzz",-1,1,1)]
        [InlineData(1,"buzz",10,-1,1)]
        [InlineData(1,"buzz",10,1,-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnError(int id,string Title,int PageCount,int GenreId,int AuthorId)
        {
            //Arrange
            UpdateBookCommand command = new(null);
            UpdateBookModel model = new UpdateBookModel(){Title=Title,AuthorId=AuthorId,GenreId=GenreId,PageCount=PageCount,PublishDate=DateTime.Now.AddYears(-10)};
            command.BookId = id;
            command.Model=model;

            //Act
            UpdateBookCommandValidator validator=new();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}