using SJI3.Core.Common.Domain;

namespace SJI3.Core.Entities;

public class TaskUnitStatus : Enumeration
{
    public static readonly TaskUnitStatus TaskStatusOne = new(1, nameof(TaskStatusOne));
    public static readonly TaskUnitStatus TaskStatusTwo = new(2, nameof(TaskStatusTwo));
    public static readonly TaskUnitStatus TaskStatusThree = new(3, nameof(TaskStatusThree));

    public TaskUnitStatus(int id, string name)
        : base(id, name)
    {
    }
}