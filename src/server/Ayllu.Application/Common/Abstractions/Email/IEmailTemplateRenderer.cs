using System.Globalization;

namespace Ayllu.Application.Common.Abstractions.Email;

public interface IEmailTemplateRenderer
{
    Task<string> RenderAsync(string templateName, object model, string culture, CancellationToken cancellation = default!);
}
