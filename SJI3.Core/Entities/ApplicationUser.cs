using CSharpFunctionalExtensions;
using SJI3.Core.Common.Domain;
using System;
using System.Collections.Generic;

namespace SJI3.Core.Entities;

public class ApplicationUser : Entity<Guid>, IAggregateRoot
{
    public sealed override Guid Id { get; protected set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }

    private List<Guid> TaskUnitsPrivate { get; set; } = new();
    public IReadOnlyCollection<Guid> TaskUnits => TaskUnitsPrivate.AsReadOnly();

    public ApplicationUser(Guid id, string userName, string password) : this()
    {
        Id = id;
        UserName = userName;
        Password = password;
    }

    protected ApplicationUser()
    {
    }
}