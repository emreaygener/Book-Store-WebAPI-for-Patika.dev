using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistsAuthorNamePlusSurnameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange (Hazırlık)
            var Author = new Author(){Name="Test_WhenAlreadyExistsAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",Surname="100",DateOfBirth=new System.DateTime(1990,01,10)};
            _context.Authors.Add(Author);
            _context.SaveChanges();

            CreateAuthorCommand command=new(_context,_mapper);
            command.Model=new(){Name=Author.Name,Surname=Author.Surname};

            //Act (Çalıştırma) & Assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yazar zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //Arrange
            CreateAuthorCommand command=new(_context,_mapper);
            AuthorsViewModel model = new(){Name="Hobbit",Surname="1000",DateOfBirth=DateTime.Now.Date.AddYears(-10)};  
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