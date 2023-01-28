using System;
using FluentValidation;

namespace WebApi.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            
            RuleFor(command => command.Model.GenreId).GreaterThan(0)
                                                     .When(command=>command.Model.GenreId!=default);
            
            RuleFor(command => command.Model.PageCount).GreaterThan(0)
                                                       .When(command=>command.Model.PageCount!=default);
            
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date)
                                                         .When(command=>command.Model.PublishDate!=default);
            
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4)
                                                   .When(command=>command.Model.Title!=default);
        }
    }
}