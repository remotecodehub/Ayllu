using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ayllu.Application.Health.Responses;

public sealed record HealthCheckResponse(IReadOnlyDictionary<string, IReadOnlyDictionary<string, object>> Entries, string Status, string Duration) : IResponse
{
    internal static HealthCheckResponse FromHealthReport(HealthReport healthReport)
    {
        // Converte cada entry para string
        var entries = healthReport.Entries.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Data);

        // Converte status agregado
        var status = healthReport.Status.ToString();

        // Formata duração total em hh:mm:ss
        var duration = $"{(int)healthReport.TotalDuration.TotalHours:D2}:" +
                       $"{healthReport.TotalDuration.Minutes:D2}:" +
                       $"{healthReport.TotalDuration.Seconds:D2}:" +
                       $"{healthReport.TotalDuration.Milliseconds} ";

        return new HealthCheckResponse(entries, status, duration);

    }
}