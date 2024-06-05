using MediatR;

namespace BuildingBlocks.Domain.Events;

public abstract record BaseEvent : INotification
{
}