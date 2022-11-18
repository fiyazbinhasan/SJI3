using System;
using CSharpFunctionalExtensions;
using SJI3.Core.Common.Domain;

namespace SJI3.Core.Entities;

public class UserMenu : Entity<Guid>, IAudit
{
    public sealed override Guid Id { get; protected set; }
    public Guid ApplicationUserId { get; private set; }
    public Guid MenuId { get; private set; }
    public bool IsActive { get; private set; }

    public UserMenu(Guid applicationUserId, Guid menuId, bool isActive)
    {
        Id = Guid.NewGuid();
        ApplicationUserId = applicationUserId;
        MenuId = menuId;
        IsActive = isActive;
    }

    #region Navigation_Properties

    public ApplicationUser ApplicationUser { get; private set; }
    public Menu Menu { get; private set; }

    #endregion
    
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