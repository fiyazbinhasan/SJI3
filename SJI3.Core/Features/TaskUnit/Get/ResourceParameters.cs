using SJI3.Core.Common.Domain;

namespace SJI3.Core.Features.TaskUnit.Get;

public class ResourceParameters : PaginationResourceParameters
{
    public string OrderBy { get; set; } = "TaskUnitStatusId";
    public string Fields { get; set; } = "";
    public string Start { get; set; } = "";
    public string End { get; set; } = "";
}