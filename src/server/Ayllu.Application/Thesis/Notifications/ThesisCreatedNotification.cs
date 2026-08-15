using Mediator.Net.Contracts;

namespace Ayllu.Application.Thesis.Notifications;

public sealed record ThesisCreatedNotification(
    string DialecticId,
    string ThesisId
) : IEvent;
