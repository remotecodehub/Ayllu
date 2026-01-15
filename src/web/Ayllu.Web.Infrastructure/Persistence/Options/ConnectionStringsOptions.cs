namespace Ayllu.Web.Infrastructure.Persistence.Options;

public sealed class ConnectionStringsOptions
{
    public const string SectionName = "ConnectionStrings";
    public string DefaultConnection { get; set; } = string.Empty;
    public string QuartzConnection { get; set; } = string.Empty;
}
