using Ayllu.Web.Application.Common.Abstractions.Storage;
using Ayllu.Web.Application.Common.Abstractions.Synthesis;
using Ayllu.Web.Application.Synthesis.Responses;
using Ayllu.Web.Domain.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ayllu.Web.Infrastructure.Common.Services.Synthesis;

using Synthesis = Domain.Entities.Dialectics.Synthesis;
using Dialectic = Domain.Entities.Dialectics.Dialectic;
public sealed class SynthesisService(IRepository<Synthesis> sr, IRepository<Dialectic> dr) : ISynthesisService
{
    public async Task<SynthesisResponse> CreateSynthesisAsync(string userId, string dialecticId, string content, CancellationToken cancellationToken)
    {
        try
        {
            return SynthesisResponse.FromEntity(await sr.CreateAsync(new Synthesis(
                dialecticId,
                userId,
                content
            ), cancellationToken));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<SynthesisListResponse> GetSynthesesByDialecticAsync(string userId, string dialecticId, CancellationToken cancellationToken)
    {
        try
        {
            var dialectic = await dr.Query().AsNoTracking()
                .Include(d => d.Syntheses)
                .Where(d => d.Id == dialecticId && d.OwnerUserId == userId)
                .SingleOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException("Dialética não encontrada para o usuario");

            var syntheses = dialectic.Syntheses.Select(SynthesisResponse.Projection).ToList();

            return SynthesisListResponse.FromSynthesesList(syntheses);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
