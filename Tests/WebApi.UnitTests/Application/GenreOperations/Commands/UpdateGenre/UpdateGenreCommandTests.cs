using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Common.ViewModels;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            _mapper=testFixture.Mapper;
        }

        [Theory]
        [InlineData(1000000)]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturned(int id)
        {
            UpdateGenreCommand command=new(_context);
            command.GenreId=id;
            FluentActions
                        .Invoking(()=>command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Kitap türü bulunamadı!");
            
        }

        [Theory]
        [InlineData(1)]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated(int id)
        {
            //Arrange
            UpdateGenreCommand command=new(_context);
            UpdateGenreViewModel model = new(){Name="Hobbit",IsActive=true};  
            command.GenreId=id;
            command.Model=model;
            //Act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            //Assert
            var Genre=_context.Genres.SingleOrDefault(Genre=>Genre.Name==model.Name);
            Genre.Should().NotBeNull();
            Genre.IsActive.Should().Be(model.IsActive);
        }
    }
}