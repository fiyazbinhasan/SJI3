using CSharpFunctionalExtensions;
using NodaTime;
using System;
using SJI3.Core.Common.Domain;

namespace SJI3.Core.Entities;

public class TaskUnit : Entity<Guid>, IAudit
{
    public sealed override Guid Id { get; protected set; }
    public string Moniker { get; private set; }
    public Instant? FromDateTime { get; private set; }
    public Instant? ToDateTime { get; private set; }
    public int TaskUnitTypeId { get; private set; }
    public int TaskUnitStatusId { get; private set; }
    public Guid ApplicationUserId { get; private set; }

    public TaskUnit(Guid id, string moniker, Instant? fromDateTime, Instant? toDateTime, int taskUnitTypeId, int taskUnitStatusId, Guid applicationUserId) : this()
    {
        Id = id;
        Moniker = moniker;
        FromDateTime = fromDateTime;
        ToDateTime = toDateTime;
        TaskUnitTypeId = taskUnitTypeId;
        TaskUnitStatusId = taskUnitStatusId;
        ApplicationUserId = applicationUserId;
    }

    protected TaskUnit()
    {
    }

    public void SetMoniker(string moniker)
    {
        Moniker = moniker;
    }

    public void SetTaskUnitType(int taskUnitTypeId)
    {
        TaskUnitTypeId = taskUnitTypeId;
    }

    public void SetTaskStatusType(int taskUnitStatusId)
    {
        TaskUnitStatusId = taskUnitStatusId;
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