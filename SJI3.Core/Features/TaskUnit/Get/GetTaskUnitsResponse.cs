using SJI3.Core.Features.Common;
using System.Collections.Generic;

namespace SJI3.Core.Features.TaskUnit.Get;

public record GetTaskUnitsResponse
{
    public PaginationMetadata PaginationMetadata { get; init; }
    public IEnumerable<TaskUnitModel> TaskUnits { get; init; }
}