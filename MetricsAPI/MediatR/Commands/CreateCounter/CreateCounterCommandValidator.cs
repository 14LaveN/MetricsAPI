using FluentValidation;
using MetricsAPI.Application.Core.Errors;
using MetricsAPI.Application.Core.Extensions;

namespace MetricsAPI.MediatR.Commands.CreateCounter;

/// <summary>
/// Represents the <see cref="CreateCounterCommand"/> validator.
/// </summary>
public sealed class CreateCounterCommandValidator
    : AbstractValidator<CreateCounterCommand>
{
    /// <summary>
    /// Validate the <see cref="CreateCounterCommand"/>.
    /// </summary>
    public CreateCounterCommandValidator()
    {
        RuleFor(x =>
                x.CounterDescription).NotEqual(string.Empty)
            .WithError(ValidationErrors.CreateCounter.DescriptionIsRequired)
            .MaximumLength(256)
            .WithMessage("Your counter description too big.");
        
        RuleFor(x =>
                x.CounterName.Value).NotEqual(string.Empty)
            .WithError(ValidationErrors.CreateCounter.NameIsRequired)
            .MaximumLength(128)
            .WithMessage("Your counter name too big.");
    }
}