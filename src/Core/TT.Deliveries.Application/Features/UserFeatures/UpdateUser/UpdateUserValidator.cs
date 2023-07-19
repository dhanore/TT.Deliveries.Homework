using FluentValidation;

namespace TT.Deliveries.Application.Features.UserFeatures.CreateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(20);
        }
    }
}
