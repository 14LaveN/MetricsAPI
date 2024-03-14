using FluentValidation;
using MetricsAPI.Application.Core.Errors;
using MetricsAPI.Application.Core.Extensions;

namespace MetricsAPI.MediatR.Commands.CreateHistogram;

/// <summary>
/// Represents the <see cref="CreateHistogramCommand"/> validator class.
/// </summary>
internal sealed class CreateHistogramCommandValidator
    : AbstractValidator<CreateHistogramCommand>
{
    /// <summary>
    /// Validate the <see cref="CreateHistogramCommand"/>.
    /// </summary>
    public CreateHistogramCommandValidator()
    {
        RuleFor(x =>
                x.HistogramDescription).NotEqual(string.Empty)
            .WithError(ValidationErrors.CreateHistogram.DescriptionIsRequired)
            .MaximumLength(256)
            .WithMessage("Your counter description too big.");
        
        RuleFor(x =>
                x.HistogramName.Value).NotEqual(string.Empty)
            .WithError(ValidationErrors.CreateHistogram.NameIsRequired)
            .MaximumLength(128)
            .WithMessage("Your counter name too big.");
    }
}