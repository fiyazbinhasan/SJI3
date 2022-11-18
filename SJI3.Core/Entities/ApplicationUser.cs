using CSharpFunctionalExtensions;
using SJI3.Core.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SJI3.Core.Entities;

public class ApplicationUser : Entity<Guid>, IAggregateRoot, IAudit
{
    public sealed override Guid Id { get; protected set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    private List<Guid> TaskUnitsPrivate { get; set; } = new();
    public IReadOnlyCollection<Guid> TaskUnits => TaskUnitsPrivate.AsReadOnly();
    
    private readonly List<UserMenu> _userMenus = new();
    public virtual IReadOnlyCollection<UserMenu> UserMenus => _userMenus.ToList();

    public ApplicationUser(Guid id, string name, string email) : this()
    {
        Id = id;
        Name = name;
        Email = email;
    }

    protected ApplicationUser()
    {
    }

    public void AddTaskUnit(Guid taskUnitId)
    {
        TaskUnitsPrivate.Add(taskUnitId);
    }
    
    public void AddMenu(UserMenu userMenu)
    {
        _userMenus.Add(userMenu);
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
}