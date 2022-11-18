using System;

namespace SJI3.Core.Features.Menu.AddUserToMenu;

public class AddUserToMenuCommand
{
    public Guid ApplicationUserId { get; set; }
    public Guid MenuId { get; set; }
}