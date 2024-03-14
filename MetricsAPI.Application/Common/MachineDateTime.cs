﻿using MetricsAPI.Application.Core.Abstractions.Common;

namespace MetricsAPI.Application.Common;

/// <summary>
/// Represents the machine date time service.
/// </summary>
internal sealed class MachineDateTime : IDateTime
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}