﻿using MediatR;

namespace HotelManagementSystem.Domain.Common;

public interface IEvent: INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}