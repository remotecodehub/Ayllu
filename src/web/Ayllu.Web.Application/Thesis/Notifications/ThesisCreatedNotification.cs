using MediatR;

namespace Ayllu.Web.Application.Thesis.Notifications;

public sealed record ThesisCreatedNotification(
    string DialecticId,
    string ThesisId
) : INotification;