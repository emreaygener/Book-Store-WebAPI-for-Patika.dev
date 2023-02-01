using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInputGivenRefersToAnEmpthyReference_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange
            DeleteBookCommand command = new(_context);
            command.BookId = _context.Books.SingleOrDefault(x => x.Id == (_context.Books.OrderByDescending(x => x.Id).First().Id)).Id + 1;

            //Act & Assertion
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Böyle bir kitap bulunamadı!");
        }


        [Fact]
        public void WhenErasedBookIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            DeleteBookCommand command = new(_context);
            command.BookId = _context.Books.SingleOrDefault(x => x.Id == (_context.Books.OrderBy(x => x.Id).First().Id)).Id;
            command.Handle();

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Böyle bir kitap bulunamadı!");
        }
    }
}