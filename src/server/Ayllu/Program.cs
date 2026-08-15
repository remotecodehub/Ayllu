using Ayllu.Application.Common.Abstractions.Culture;
using Ayllu.Application.Common.Utils;
using Ayllu.Components;
using Ayllu.Domain.Entities.Identity;
using Ayllu.Extensions;
using Ayllu.Filters;
using Ayllu.Helpers;
using Ayllu.Infrastructure.HealthChecks.Api;
using Ayllu.Infrastructure.HealthChecks.Database;
using Ayllu.Middlewares;
using Ayllu.Transformers;
using Ayllu.Composition;
using Scalar.AspNetCore;
using System.Diagnostics;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/v1/identity")
    .AddEndpointFilter<IdentityResultEnvelopeFilter>()
    .MapIdentityApi<ApplicationUser>()
    .DisableAntiforgery()
    .WithTags("Identity");
app.MapControllers();
 
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

await app.RunAsync(lifetime.ApplicationStopping);
