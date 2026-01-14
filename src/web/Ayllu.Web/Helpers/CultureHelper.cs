using Ayllu.Web.Application.Common.Abstractions.Culture;

namespace Ayllu.Web.Helpers;

/// <summary>
/// Culture helper implementation
/// </summary>
/// <param name="accessor"></param>
public sealed class CultureHelper(IHttpContextAccessor accessor) : ICultureHelper
{
    /// <summary>
    /// Retrieves the culture information from the current HTTP request's Accept-Language header.
    /// </summary>
    /// <remarks>This method is typically used to determine the preferred culture of the client making the
    /// HTTP request. If the Accept-Language header is missing or contains only whitespace, the method defaults to
    /// "pt-BR".</remarks>
    /// <returns>A string representing the culture specified in the Accept-Language header of the current HTTP request. Returns
    /// "pt-BR" if the header is not present or is empty.</returns>
    public string GetCurrentCulture()
    {
        var culture = accessor.HttpContext?.Request.Headers.AcceptLanguage.ToString();
        if (string.IsNullOrWhiteSpace(culture) || string.IsNullOrEmpty(culture))
        {
            return "pt-BR";
        }
        return culture;
    }
}
