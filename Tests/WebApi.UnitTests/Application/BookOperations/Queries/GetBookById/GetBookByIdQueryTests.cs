using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Queries.GetBookById;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.BookOperations.Queries.GetBookById
{
    public class GetBookByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookByIdQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetBookByIdQuery query = new(_context,_mapper);
            query.BookId=_context.Books.OrderByDescending(x=>x.Id).First().Id+1;

            FluentActions
                .Invoking(()=>query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap mevcut değil!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //Arrange
            GetBookByIdQuery query=new(_context,_mapper);
            BooksViewModel model = new();
            query.BookId=_context.Books.OrderBy(x=>x.Id).First().Id;
            //Act
            FluentActions.Invoking(()=>query.Handle()).Invoke();
            //Assert
            var book=_context.Books.SingleOrDefault(book=>book.Id==query.BookId);
            book.Should().NotBeNull();
        }
    }
}