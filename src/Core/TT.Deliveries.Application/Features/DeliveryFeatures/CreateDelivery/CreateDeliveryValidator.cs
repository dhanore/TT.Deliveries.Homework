using FluentValidation;
namespace TT.Deliveries.Application.Features.DeliveryFeatures;
public class CreateDeliveryValidator : AbstractValidator<CreateDeliveryRequest>
{
    public CreateDeliveryValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().MinimumLength(2);
    }
}
