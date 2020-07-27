#region "Libraries"

using FluentValidation;
using MPIS.User.AplicationService.DTOs.User;
using System;

#endregion

namespace MPIS.User.FluentValidation.Validation.User
{
    public class CreateDeleteUserByIdRequestValidator : AbstractValidator<DeleteUserByIdRequest>
    {
        public CreateDeleteUserByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).NotEqual(default(Guid)).WithMessage("Guid Not Defined");
        }
    }
}
