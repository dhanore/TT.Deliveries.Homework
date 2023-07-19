using FluentValidation;

namespace TT.Deliveries.Application.Common
{
    public sealed class ValidationBehavior<TRequest,TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public TResponse Handle(TRequest request, TResponse next)
        {
            if (!_validators.Any()) return next;

            var context = new ValidationContext<TRequest>(request);

            var errors = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .Select(x => x.ErrorMessage)
                .Distinct()
                .ToArray();

            if (errors.Any())
                throw new BadRequestException(errors);

            return next;
        }
    }
}
