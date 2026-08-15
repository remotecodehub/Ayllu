namespace Ayllu.Application.Dialectics.Requests;

public sealed record CreateDialecticRequest(
    string Title,
    string Description,
    bool IsPublic
);