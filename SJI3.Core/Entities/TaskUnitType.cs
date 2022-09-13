using SJI3.Core.Common.Domain;

namespace SJI3.Core.Entities;

public class TaskUnitType : Enumeration
{
    public static readonly TaskUnitType TypeOne = new(1, nameof(TypeOne));
    public static readonly TaskUnitType TypeTwo = new(2, nameof(TypeTwo));

    public TaskUnitType(int id, string name)
        : base(id, name)
    {
    }
}