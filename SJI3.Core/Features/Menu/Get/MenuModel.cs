using System;

namespace SJI3.Core.Features.Menu.Get;

public record MenuModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Icon { get; set; }
    public Guid? ParentId { get; set; }
    public bool IsParent { get; set; }
    public bool IsActive { get; set; }
}