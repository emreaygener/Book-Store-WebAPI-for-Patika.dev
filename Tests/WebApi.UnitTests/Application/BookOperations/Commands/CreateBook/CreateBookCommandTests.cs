using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange (Hazırlık)
            var book = new Book(){Title="Test_WhenAlreadyExistsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",PageCount=100,PublishDate=new System.DateTime(1990,01,10),GenreId=1,AuthorId=1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command=new(_context,_mapper);
            command.Model=new(){Title=book.Title};

            //Act (Çalıştırma) & Assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //Arrange
            CreateBookCommand command=new(_context,_mapper);
            CreateBookModel model = new(){Title="Hobbit",PageCount=1000,PublishDate=DateTime.Now.Date.AddYears(-10),GenreId=1,AuthorId=1};  
            command.Model=model;
            //Act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            //Assert
            var book=_context.Books.SingleOrDefault(book=>book.Title==model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);

        }


    }
}