using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenExecuted_Books_ShouldBeReturn()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);

            FluentActions
                        .Invoking(() => query.Handle())
                        .Should().NotThrow<System.Exception>();
        }
    }
}