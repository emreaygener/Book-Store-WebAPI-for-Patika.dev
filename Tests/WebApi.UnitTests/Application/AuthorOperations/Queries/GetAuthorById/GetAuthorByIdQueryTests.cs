using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorById;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.AuthorOperations.Queries.GetAuthorById
{
    public class GetAuthorByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorByIdQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetAuthorByIdQuery query = new(_context,_mapper);
            query.AuthorId=_context.Authors.OrderByDescending(x=>x.Id).First().Id+1;

            FluentActions
                .Invoking(()=>query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yazar zaten mevcut!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //Arrange
            GetAuthorByIdQuery query=new(_context,_mapper);
            AuthorsViewModel model = new();
            query.AuthorId=_context.Authors.OrderBy(x=>x.Id).First().Id;
            //Act
            FluentActions.Invoking(()=>query.Handle()).Invoke();
            //Assert
            var Author=_context.Authors.SingleOrDefault(Author=>Author.Id==query.AuthorId);
            Author.Should().NotBeNull();
        }
    }
}