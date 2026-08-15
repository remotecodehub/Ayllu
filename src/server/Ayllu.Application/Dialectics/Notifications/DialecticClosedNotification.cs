using MediatR;

namespace Ayllu.Application.Dialectics.Notifications;

public sealed record DialecticClosedNotification(
    string DialecticId
) : INotification;
