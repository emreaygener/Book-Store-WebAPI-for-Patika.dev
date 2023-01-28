using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteBook
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command=>command.AuthorId).GreaterThan(0);
        }
    }
}