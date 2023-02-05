using FluentValidation;

namespace WebApi.Applications.UserOperations.Commands.CreateUser
{
    class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(1);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(1);
            RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.Model.Password).NotEmpty().NotNull().MinimumLength(6).MaximumLength(32);
        }
    }
}