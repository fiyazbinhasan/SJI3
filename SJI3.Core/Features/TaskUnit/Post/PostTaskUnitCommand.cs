using NodaTime;
using System;

namespace SJI3.Core.Features.TaskUnit.Post;

public class PostTaskUnitCommand
{
    public Guid Id { get; set; }
    public string Moniker { get; set; }
    public int TaskUnitTypeId { get; set; }
    public Instant? FromDateTime { get; set; }
    public Instant? ToDateTime { get; set; }
    public Guid ApplicationUserId { get; set; }
}