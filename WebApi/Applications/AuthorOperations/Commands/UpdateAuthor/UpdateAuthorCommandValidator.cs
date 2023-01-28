using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateBook
{
    public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command=>command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(2).When(x => x.Model.Name.Trim() != string.Empty || x.Model.Name.Trim() != "string");
            RuleFor(command => command.Model.Surname).MinimumLength(2).When(x => x.Model.Surname.Trim() != string.Empty || x.Model.Surname.Trim() != "string");
            
            
        }
    }
}