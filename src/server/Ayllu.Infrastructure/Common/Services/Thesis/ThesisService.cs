using Ayllu.Application.Common.Abstractions.Storage;
using Ayllu.Application.Common.Abstractions.Thesis;
using Ayllu.Application.Thesis.Responses;
using Ayllu.Domain.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Ayllu.Infrastructure.Common.Services.Thesis;

using Thesis = Domain.Entities.Dialectics.Thesis;
using Dialectic = Domain.Entities.Dialectics.Dialectic;

public sealed class ThesisService(IRepository<Thesis> tr, IRepository<Dialectic> dr) : IThesisService
{
    public async Task<ThesisResponse> CreateThesisAsync(string content, string userId, string dialecticId, CancellationToken cancellationToken)
    {
        try
        {
            var dialectic = await dr.Query()
                .Where(d => d.Id == dialecticId && d.OwnerUserId == userId)
                .SingleOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("dialetica não encontrada para o usuario"); 
            var thesis = new Thesis(content, userId, dialecticId);
            var result = await tr.CreateAsync(thesis, cancellationToken) ?? throw new InvalidOperationException("Tese não foi criada");
            dialectic.PublishThesis(thesis);
            _ = await dr.UpdateAsync(dialectic, cancellationToken);
            return ThesisResponse.FromEntity(result);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ThesisResponse> UpdateThesisAsync(string content, string userId, string dialecticId, string thesisId, CancellationToken cancellationToken)
    {
        try
        {
            var thesis = await tr.Query().Where(t => t.DialecticId == dialecticId && t.AuthorUserId == userId && t.Id == thesisId).SingleOrDefaultAsync(cancellationToken) 
                ?? throw new EntityNotFoundException("Não foi possível buscar teses para o usuário");
            thesis!.UpdateContent(content);
            var result = await tr.UpdateAsync(thesis, cancellationToken);
            return ThesisResponse.FromEntity(result);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
