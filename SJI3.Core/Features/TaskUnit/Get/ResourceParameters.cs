using NodaTime;
using SJI3.Core.Common.Domain;
using System;

namespace SJI3.Core.Features.TaskUnit.Get;

public class ResourceParameters : PaginationResourceParameters
{
    public string OrderBy { get; set; } = "TaskUnitStatusId";
    public string Fields { get; set; } = "";
    public LocalDate? Start { get; set; }
    public LocalDate? End { get; set; }
}