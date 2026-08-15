using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Common.Abstractions.Storage;
using Ayllu.Application.Dialectics.Responses;
using Ayllu.Domain.Exceptions.Dialetics;
using Ayllu.Domain.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ayllu.Infrastructure.Common.Services.Dialectic;

using Dialectic = Domain.Entities.Dialectics.Dialectic;

public sealed class DialecticService(IRepository<Dialectic> dialecticRepository, ILogger<DialecticService> logger) : IDialecticService
{
    public async Task<DialecticResponse> CloseDialecticAsync(string id, string userId, CancellationToken cancellationToken)
    {
        try
        {
            var dialectic = (await dialecticRepository.Query()
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Id == id && d.OwnerUserId == userId, cancellationToken))
                ?? throw new EntityNotFoundException("Dialética não encontrada para o identificador e usuário atual");
            
            dialectic?.Close();
            
            _ = await dialecticRepository.UpdateAsync(dialectic!, cancellationToken);
            return DialecticResponse.FromEntity(dialectic!);
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }

    public async Task<DialecticResponse> CreateDialecticAsync(string title, string description, bool isPublic, string userId, CancellationToken cancellationToken)
    {
        try
        {
            var dialectic = new Dialectic(title, userId)
            {
                Description = description,
                IsPublic = isPublic
            };

            DialecticException.ThrowIfAlreadyExists(await dialecticRepository
                .Query()
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Title == title && d.OwnerUserId == userId, cancellationToken));

            return DialecticResponse.FromEntity(await dialecticRepository.CreateAsync(dialectic, cancellationToken));
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }

    public async Task<DialecticResponse> GetDialecticByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            return (await dialecticRepository
                .Query()
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(DialecticResponse.Projection)
                .ToListAsync(cancellationToken))
                .SingleOrDefault() ?? throw new EntityNotFoundException("Dialética não encontrada para o identificador");
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }

    public async Task<IReadOnlyList<DialecticSummaryResponse>> GetMyDialecticsAsync(string userId, CancellationToken cancellationToken)
    {
        try
        {
            var dialectics = await dialecticRepository
                .Query()
                .AsNoTracking()
                .Where(d => d.OwnerUserId == userId)
                .Select(DialecticSummaryResponse.Projection)
                .ToListAsync(cancellationToken);
            return dialectics.Count > 0 ? dialectics : throw new EntityNotFoundException("Dialéticas do usuário não encontradas");
        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }

    public async Task<DialecticListResponse> GetDialecticsListAsync(CancellationToken cancellationToken)
    {
        try
        {
            var dialectics = await dialecticRepository
                .Query()
                .AsNoTracking()
                .Include(d => d.Thesis)
                .Include(d => d.Antitheses)
                .Include(d => d.Syntheses)
                .Select(DialecticResponse.Projection)
                .ToListAsync(cancellationToken);

            return dialectics.Count > 0 ? DialecticListResponse.FromEntityList(dialectics) : throw new EntityNotFoundException("Dialéticas não encontradas");

        }
        catch (Exception e)
        {
            logger.LogError(e, "{Message}", e.Message);
            throw;
        }
    }
}
