using FluentValidation;

namespace Viseo.Application.Request.UserRequest.Command.DeleteUser
{
    public class DeleteUserRequestCommandValidation : AbstractValidator<DeleteUserRequestCommand>
    {
        public DeleteUserRequestCommandValidation()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}