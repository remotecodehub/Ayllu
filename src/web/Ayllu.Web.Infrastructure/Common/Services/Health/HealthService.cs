using Ayllu.Web.Application.Common;
using Ayllu.Web.Application.Common.Abstractions.Health;
using Ayllu.Web.Application.Common.Constants;
using Ayllu.Web.Application.Health.Responses;
using Ayllu.Web.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Ayllu.Web.Infrastructure.Common.Services.Health;

public sealed class HealthService : IHealthService
{
    private readonly Process _process;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<HealthService> _logger;
    private readonly HealthCheckService _healthCheckService;
    private TimeSpan _lastCpuTime;
    private DateTime _lastCheck;

    public HealthService(ApplicationDbContext db, HealthCheckService healthCheckService, ILogger<HealthService> logger)
    {
        _process = Process.GetCurrentProcess();
        _lastCpuTime = _process.TotalProcessorTime;
        _lastCheck = DateTime.UtcNow;
        _db = db;
        _healthCheckService = healthCheckService;
        _logger = logger;
    }

    public async Task<HealthReport> PerformDbReadinessCheckAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(PerformDbReadinessCheckAsync)} starts");
            return await _healthCheckService.CheckHealthAsync(r => r.Name == "DatabaseCheck", cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Message}", e.Message);
            throw;
        }
        finally
        {
            _logger.LogInformation($"{nameof(PerformDbReadinessCheckAsync)} finishes");
        }
    }

    public async Task<HealthReport> GetDbDiskUsageAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetDbDiskUsageAsync)} starts");
            return await _healthCheckService.CheckHealthAsync(r => r.Name == "DatabaseUsageCheck", cancellationToken);

        }
        catch (Exception e)
        {
            _logger.LogInformation(e, "{Message}", e.Message);
            throw;
        }
        finally
        {
            _logger.LogInformation($"{nameof(GetDbDiskUsageAsync)} end");
        }
    }
     
    public async Task<HealthReport> GetApiDiskUsageAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetApiDiskUsageAsync)} starts");
            return await _healthCheckService.CheckHealthAsync(r => r.Name == "ApiDiskUsageCheck", cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Message}", e.Message);
            throw;
        }
        finally
        {
            _logger.LogInformation($"{nameof(GetApiDiskUsageAsync)} ends");
        }
    }

    public async Task<HealthReport> GetHostInfoAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetHostInfoAsync)} starts");
            return await _healthCheckService.CheckHealthAsync(r => r.Name == "HostInfoCheck", cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetHostInfoAsync has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            _logger.LogInformation($"{nameof(GetHostInfoAsync)} ends");
        }
    }

    public async Task<HealthReport> GetHealthCheckAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetHealthCheckAsync)} ends");
            return await _healthCheckService.CheckHealthAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetHealthCheckAsync has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            _logger.LogInformation($"{nameof(GetHealthCheckAsync)} ends");
        }
    }
}
