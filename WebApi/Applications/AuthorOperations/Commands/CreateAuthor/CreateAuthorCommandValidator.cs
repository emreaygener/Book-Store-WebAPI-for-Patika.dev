using System;
using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.CreateBook
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2);
            RuleFor(command => command.Model.Surname).MinimumLength(2).When(x => x.Model.Surname.Trim() != string.Empty || x.Model.Surname.Trim() != "string");
            RuleFor(command => command.Model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}