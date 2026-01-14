using Ayllu.Web.Application.Common.Abstractions.Culture;
using Ayllu.Web.Composition;
using Ayllu.Web.Domain.Entities.Identity;
using Ayllu.Web.Extensions;
using Ayllu.Web.Filters;
using Ayllu.Web.Helpers;
using Ayllu.Web.Middlewares;
using Ayllu.Web.Transformers;
using MudBlazor.Services;
using Scalar.AspNetCore;

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
});
builder.Services.AddAylluModule(builder.Configuration);
builder.Services.AddSingleton<ICultureHelper, CultureHelper>(); ;
builder.Services.AddHealthChecks();
if (!builder.Environment.IsProduction())
{
    builder.Logging.AddConsole().AddDebug();
}
var app = builder.Build();
app.UseGlobalExceptionHandler();

await app.TryMigrateDbAsync();

app.MapOpenApi("/openapi/{v1}.json");

app.MapScalarApiReference("scalar/ayllu", options =>
{
    options.Title = "Ayllu API";
    options.Theme = ScalarTheme.DeepSpace;
    options.DotNetFlag = true;
    options.AddHeadContent("<h3>Ayllu é uma solução open source para gerenciamento demovimentos de organizações e entidades sociais. Promove a discussão entre os usuários em forma de dialética</h3>");
    options.HideSearch = false; 
}).RequireAuthorization("ScalarPolicy");


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

app.MapHealthChecks("/api/v1/health/check");

await app.RunAsync();