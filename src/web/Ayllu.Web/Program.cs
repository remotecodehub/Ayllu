using Ayllu.Web.Application.Common.Abstractions.Culture;
using Ayllu.Web.Application.Common.Constants;
using Ayllu.Web.Composition;
using Ayllu.Web.Domain.Entities.Identity;
using Ayllu.Web.Extensions;
using Ayllu.Web.Filters;
using Ayllu.Web.Helpers;
using Ayllu.Web.Infrastructure.HealthChecks.Api;
using Ayllu.Web.Infrastructure.HealthChecks.Database;
using Ayllu.Web.Middlewares;
using Ayllu.Web.Transformers; 
using Scalar.AspNetCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionResultFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddOperationTransformer<IdentityOpenApiTransformer>();
    options.AddScalarTransformers();
});
builder.Services.AddAylluModule(builder.Configuration);
builder.Services.AddSingleton<ICultureHelper, CultureHelper>(); ;
builder.Services
    .AddHealthChecks()
    .AddCheck<ApiCheck>("ApiCheck")
    .AddCheck<ApiDiskUsageCheck>("ApiDiskUsageCheck")
    .AddCheck<HostInfoCheck>("HostInfoCheck")
    .AddCheck<DatabaseCheck>("DatabaseCheck")
    .AddCheck<DatabaseUsageCheck>("DatabaseUsageCheck");
    
if (!builder.Environment.IsProduction())
{
    builder.Logging.AddConsole().AddDebug();
}
var app = builder.Build();
AylluUtils.SetEnvironmentName(app.Environment.EnvironmentName);
app.UseGlobalExceptionHandler();

await app.TryMigrateDbAsync();

app.MapOpenApi("/openapi/{v1}.json");

app.MapScalarApiReference("scalar/ayllu", options =>
{
    options.Title = $"Ayllu API ({app.Environment.EnvironmentName})";
    options.Theme = ScalarTheme.DeepSpace;
    options.DotNetFlag = true;
    options.HideSearch = false;
    options.ShowOperationId()
        .ExpandAllTags()
        .ExpandAllResponses()
        .HideDeveloperTools()
        .HideModels()
        .WithSearchHotKey("s")
        .SortTagsAlphabetically()
        .SortOperationsByMethod()
        .PreserveSchemaPropertyOrder();
});

if (!app.Environment.IsProduction())
{
    await app.Seed();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapGroup("/api/v1/identity")
    .AddEndpointFilter<IdentityResultEnvelopeFilter>()
    .MapIdentityApi<ApplicationUser>()
    .DisableAntiforgery()
    .WithTags("Identity");
app.MapControllers();
 
await app.RunAsync(); 
