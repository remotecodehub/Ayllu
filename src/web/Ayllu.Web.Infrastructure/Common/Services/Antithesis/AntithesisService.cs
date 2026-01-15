using Ayllu.Web.Application.Antithesis.Responses;
using Ayllu.Web.Application.Common.Abstractions.Antithesis;
using Ayllu.Web.Application.Common.Abstractions.Storage;
using Ayllu.Web.Domain.Exceptions.Common;
using Ayllu.Web.Domain.Exceptions.Dialetics;
using Microsoft.EntityFrameworkCore;

namespace Ayllu.Web.Infrastructure.Common.Services.Antithesis;

using Antithesis = Domain.Entities.Dialectics.Antithesis;
using Dialectic = Domain.Entities.Dialectics.Dialectic;

public sealed class AntithesisService(IRepository<Antithesis> atr, IRepository<Dialectic> dr) : IAntithesisService
{
    public async Task<AntithesisResponse> CreateAntithesisAsync(string userId, string dialecticId, string content, CancellationToken cancellationToken)
    {
		try
		{
			DialecticException.ThrowIfNotExists(await dr.Query().AsNoTracking().SingleOrDefaultAsync(d => d.Id == dialecticId && d.OwnerUserId == userId, cancellationToken));
			
			return AntithesisResponse.FromEntity(await atr.CreateAsync(new Antithesis(content, userId, dialecticId), cancellationToken));
		}
		catch (Exception)
		{
			throw;
		}
    }

	public async Task<AntithesisListResponse> GetAntithesesByDialectAsync(string userId, string dialecticId, bool isPublic, CancellationToken cancellationToken)
	{
		try
		{
			var dialectic = await dr.Query()
				.AsNoTracking()
				.Include(d => d.Antitheses)
				.Where(d => d.Id == dialecticId && d.OwnerUserId == userId && d.IsPublic == isPublic)
				.SingleOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("Dialética não encontrada para o usuário");

			return dialectic.Antitheses.Count > 0
				? AntithesisListResponse.FromAntithesesList([.. dialectic.Antitheses.Select(AntithesisResponse.Projection)])
                :              throw new EntityNotFoundException("Antiteses nao encontradas para dialetica");
        }
		catch (Exception)
		{
			throw;
		}
	}
}
