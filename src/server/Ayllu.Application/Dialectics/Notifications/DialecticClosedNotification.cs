using Mediator.Net.Contracts;

namespace Ayllu.Application.Dialectics.Notifications;

public sealed record DialecticClosedNotification(
    string DialecticId
) : IEvent;
