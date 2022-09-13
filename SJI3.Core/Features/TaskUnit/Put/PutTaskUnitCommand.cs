using System;

namespace SJI3.Core.Features.TaskUnit.Put;

public class PutTaskUnitCommand
{
    public Guid Id { get; set; }
    public int TaskUnitStatusId { get; set; }
}