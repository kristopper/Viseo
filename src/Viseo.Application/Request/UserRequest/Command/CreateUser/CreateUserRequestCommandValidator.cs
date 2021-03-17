using FluentValidation;

namespace Viseo.Application.Request.UserRequest.Command.CreateUser
{
    public class CreateUserRequestCommandValidator : AbstractValidator<CreateUserRequestCommand>
    {
        public CreateUserRequestCommandValidator()
        {
            RuleFor(v => v.Password).NotEmpty();
            RuleFor(v => v.FirstName).NotEmpty();
            RuleFor(v => v.LastName).NotEmpty();
            RuleFor(v => v.Role).NotEmpty();

            RuleFor(v => v.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(v => v.Username)
                .NotEmpty()
                .MinimumLength(5);
        }
    }
}