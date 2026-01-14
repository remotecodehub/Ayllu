using MediatR;

namespace Ayllu.Web.Application.Dialectics.Notifications;

public sealed record DialecticClosedNotification(
    string DialecticId
) : INotification;
