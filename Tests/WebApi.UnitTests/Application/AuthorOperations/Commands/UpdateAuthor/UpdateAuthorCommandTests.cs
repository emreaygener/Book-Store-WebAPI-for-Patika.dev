using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            _mapper=testFixture.Mapper;
        }

        [Theory]
        [InlineData(1000000)]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned(int id)
        {
            UpdateAuthorCommand command=new(_context);
            command.AuthorId=id;
            FluentActions
                        .Invoking(()=>command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Kitap mevcut değil!");
            
        }

        [Theory]
        [InlineData(1)]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated(int id)
        {
            //Arrange
            UpdateAuthorCommand command=new(_context);
            AuthorsViewModel model = new(){Name="Hobbit",Surname="1000",DateOfBirth=DateTime.Now.Date.AddYears(-10)};  
            command.AuthorId=id;
            command.Model=model;
            //Act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            //Assert
            var Author=_context.Authors.SingleOrDefault(Author=>Author.Name==model.Name);
            Author.Should().NotBeNull();
            Author.Surname.Should().Be(model.Surname);
            Author.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}