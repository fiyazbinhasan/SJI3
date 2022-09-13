using NodaTime;
using System;

namespace SJI3.Core.Features.TaskUnit.Get;

public record TaskUnitModel
{
    public Guid Id { get; set; }
    public string Moniker { get; set; }
    public LocalDateTime FromDateTime { get; set; }
    public LocalDateTime ToDateTime { get; set; }
    public string TaskUnitType { get; set; }
    public string TaskUnitStatus { get; set; }
}