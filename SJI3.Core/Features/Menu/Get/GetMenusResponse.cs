using System.Collections.Generic;

namespace SJI3.Core.Features.Menu.Get;

public class GetMenusResponse
{
    public IEnumerable<MenuModel> Menus { get; init; }
}