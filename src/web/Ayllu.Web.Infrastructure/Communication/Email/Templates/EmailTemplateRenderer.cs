using Ayllu.Web.Application.Common.Abstractions.Email; 

namespace Ayllu.Web.Infrastructure.Communication.Email.Templates;


public sealed class EmailTemplateRenderer : IEmailTemplateRenderer
{
    private readonly System.Reflection.Assembly assembly = typeof(EmailTemplateRenderer).Assembly;
    public async Task<string> RenderAsync(
        string templateName,
        object model,
        string culture,
        CancellationToken cancellationToken = default!)
    { 
        var resourceName = $"Ayllu.Web.Infrastructure.Communication.Email.TemplatesFiles.{culture}.{templateName}.html";

        await using var stream = assembly.GetManifestResourceStream(resourceName) ?? throw new FileNotFoundException($"Template not found: {resourceName}");
        
        using var reader = new StreamReader(stream);
        
        var template = await reader.ReadToEndAsync(cancellationToken);

        foreach (var prop in model.GetType().GetProperties())
        {
            template = template.Replace(
                $"{{{{{prop.Name}}}}}",
                prop.GetValue(model)?.ToString() ?? string.Empty);
        }

        return template;
    }
}