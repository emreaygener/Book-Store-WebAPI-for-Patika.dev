using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistsGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange (Hazırlık)
            var Genre = new Genre(){Name="Test_WhenAlreadyExistsGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"};
            _context.Genres.Add(Genre);
            _context.SaveChanges();

            CreateGenreCommand command=new(_context);
            command.Model=new(){Name=Genre.Name};

            //Act (Çalıştırma) & Assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap zaten mevcut.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //Arrange
            CreateGenreCommand command=new(_context);
            CreateGenreModel model = new(){Name="Hobbit"};  
            command.Model=model;
            //Act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            //Assert
            var Genre=_context.Genres.SingleOrDefault(Genre=>Genre.Name==model.Name);
            Genre.Should().NotBeNull();
        }


    }
}