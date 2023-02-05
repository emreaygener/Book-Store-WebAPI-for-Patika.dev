using System;

namespace WebApi.Common
{
    public class ViewModels
    {
        public class BooksViewModel
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int AuthorId { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int AuthorId { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }

        public class GenresViewModel
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        public class CreateGenreModel
        {
            public string Name { get; set; }
        }

        public class UpdateGenreViewModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; } = true;
        }

        public class AuthorsViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
        }

        public class CreateUserModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsersViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
        }
    }
}