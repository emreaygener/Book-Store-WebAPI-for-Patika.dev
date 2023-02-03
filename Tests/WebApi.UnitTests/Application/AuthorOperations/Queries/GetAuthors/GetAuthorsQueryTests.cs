using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenExecuted_Authors_ShouldBeReturn()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);

            FluentActions
                        .Invoking(() => query.Handle())
                        .Should().NotThrow<System.Exception>();
        }
    }
}