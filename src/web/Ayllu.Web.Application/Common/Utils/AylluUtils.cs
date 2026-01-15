using System.Diagnostics;

namespace Ayllu.Web.Application.Common.Constants;


public static class AylluUtils
{
    private static readonly Process _api = Process.GetCurrentProcess();
    private static string? EnvironmentName { get; set; }  
    public static string GetUpTime()
    {
        var span = DateTimeOffset.UtcNow - _api.StartTime;
        if (span.Days > 0)
            return $"{span.Days}d {span.Hours:D2}h:{span.Minutes:D2}m:{span.Seconds:D2}s";

        return $"{span.Hours:D2}h:{span.Minutes:D2}m:{span.Seconds:D2}s";
    }

    public static string GetEnvironmentName() => EnvironmentName ?? "Unknown";
    public static void SetEnvironmentName(string name) => EnvironmentName = name;

}
