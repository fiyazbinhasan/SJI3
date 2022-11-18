using CSharpFunctionalExtensions;
using SJI3.Core.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SJI3.Core.Entities;

public class Menu : Entity<Guid>, IAudit
{
    public sealed override Guid Id { get; protected set; }
    public string Title { get;  private set; }
    public string Url { get; private set; }
    public string Icon { get; private set; }
    public Guid? ParentId { get; private set; }
    public bool IsParent { get; private set; }
    public bool IsActive { get; private set; }
    
    private readonly List<UserMenu> _userMenus = new();
    public IReadOnlyCollection<UserMenu> UserMenus => _userMenus.ToList();

    public Menu(Guid id, string title, string url, string icon, Guid? parentId, bool isParent, bool isActive)
    {
        Id = id;
        Title = title;
        Url = url;
        Icon = icon;
        ParentId = parentId;
        IsParent = isParent;
        IsActive = isActive;
    }

    #region Audit_Properties

    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset? ModifiedOn { get; private set; }

    public void SetCreatedOn(DateTimeOffset dateTimeOffset)
    {
        CreatedOn = dateTimeOffset;
    }

    public void SetModifiedOn(DateTimeOffset dateTimeOffset)
    {
        ModifiedOn = dateTimeOffset;
    }

    #endregion Audit_Properties
    
    public void AddUser(ApplicationUser applicationUser)
    {
        var userMenu = new UserMenu(applicationUser.Id, Id, IsActive);
        _userMenus.Add(userMenu);
        applicationUser.AddMenu(userMenu);
    }
}
