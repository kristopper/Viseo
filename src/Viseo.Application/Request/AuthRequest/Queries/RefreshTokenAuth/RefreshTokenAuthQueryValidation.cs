using FluentValidation;

namespace Viseo.Application.AuthRequest.Queries.RefreshTokenAuth
{
    public class RefreshTokenAuthQueryValidation : AbstractValidator<RefreshTokenAuthQuery>
    {

        public RefreshTokenAuthQueryValidation()
        {
            RuleFor(v => v.refreshToken).NotEmpty();
        }
    }

}