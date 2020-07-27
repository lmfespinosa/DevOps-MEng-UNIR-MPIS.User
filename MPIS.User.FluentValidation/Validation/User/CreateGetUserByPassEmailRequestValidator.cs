#region "Libraries"

using FluentValidation;
using MPIS.User.AplicationService.DTOs.User;

#endregion

namespace MPIS.User.FluentValidation.Validation.User
{
    public class CreateGetUserByPassEmailRequestValidator : AbstractValidator<GetUserByPassEmailRequest>
    {
        public CreateGetUserByPassEmailRequestValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
