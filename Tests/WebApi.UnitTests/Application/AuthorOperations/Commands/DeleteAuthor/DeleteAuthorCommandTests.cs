using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInputGivenRefersToAnEmpthyReference_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange
            DeleteAuthorCommand command = new(_context);
            command.AuthorId = _context.Authors.SingleOrDefault(x => x.Id == (_context.Authors.OrderByDescending(x => x.Id).First().Id)).Id + 1;

            //Act & Assertion
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yazar bulunamadı!");
        }


        [Fact]
        public void WhenErasedAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            DeleteAuthorCommand command = new(_context);
            Author author = new Author();
            author.Name = "test";
            author.Surname = "testt";
            author.DateOfBirth = DateTime.Now.AddYears(-15);
            _context.Authors.Add(author);
            _context.SaveChanges();
            command.AuthorId = _context.Authors.OrderByDescending(x => x.Id).First().Id;
            command.Handle();

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yazar bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
        {
            DeleteAuthorCommand command = new(_context);
            Author author = new Author();
            author.Name = "test";
            author.Surname = "testt";
            author.DateOfBirth = DateTime.Now.AddYears(-15);
            _context.Authors.Add(author);
            _context.SaveChanges();

            //Arrange
            command.AuthorId = _context.Authors.OrderByDescending(x => x.Id).First().Id;
            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //Assert
            var Author = _context.Authors.SingleOrDefault(Author => Author.Id == command.AuthorId);
            Author.Should().BeNull();
        }
    }
}