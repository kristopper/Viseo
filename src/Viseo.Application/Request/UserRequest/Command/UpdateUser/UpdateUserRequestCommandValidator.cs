using FluentValidation;

namespace Viseo.Application.Request.UserRequest.Command.UpdateUser
{
    public class UpdateUserRequestCommandValidator : AbstractValidator<UpdateUserRequestCommand>
    {
        public UpdateUserRequestCommandValidator()
        {
            RuleFor(v => v.FirstName).NotEmpty();
            RuleFor(v => v.LastName).NotEmpty();
            RuleFor(v => v.Password).NotEmpty();
            RuleFor(v => v.Role).NotEmpty();
            RuleFor(v => v.Status).NotEmpty();
 
            RuleFor(v => v.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(v => v.Username)
                .NotEmpty()
                .MinimumLength(5);
        }
    }
}