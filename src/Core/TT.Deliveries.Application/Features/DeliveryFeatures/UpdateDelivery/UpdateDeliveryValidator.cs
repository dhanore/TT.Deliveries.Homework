using FluentValidation;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public class UpdateDeliveryValidator : AbstractValidator<UpdateDeliveryRequest>
{
    public UpdateDeliveryValidator()
    {
        RuleFor(x => x.State).NotEmpty();
    }
}
