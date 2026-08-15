using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Common.Abstractions.UnitOfWork;
using Ayllu.Domain.Entities.Identity;
using Ayllu.Infrastructure.Persistence.Data;
using Ayllu.Domain.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Ayllu.Infrastructure.Common.Repositories.Identity;

public sealed class ApplicationUserFriendshipRepository(ApplicationDbContext db, IUnitOfWork uow, ILogger<ApplicationUserFriendshipRepository> logger) : IApplicationUserFriendshipRepository
{
    public async Task<bool> AnyAsync(Expression<Func<ApplicationUserFriendship, bool>> predicate, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"{nameof(AnyAsync)} starts");
            return await db.UserFriendships.AnyAsync(predicate, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "AnyAsync Has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(AnyAsync)} finishes");
        }
    }

    public async Task<ApplicationUserFriendship> CreateAsync(ApplicationUserFriendship entity, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"{nameof(CreateAsync)} starts");
            var entry = await db.UserFriendships.AddAsync(entity, cancellationToken);
            if (entry.State != EntityState.Added)
                throw new RepositoryException("Não foi possível adicionar o vinculo de amizade");
            var result = await uow.SaveChangesAsync(cancellationToken);
            return result > 0 ? entry.Entity : throw new RepositoryException("Alterações não foram salvas");
        }
        catch (Exception e)
        {
            logger.LogError(e, "CreateAsync Has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(CreateAsync)} finishes");
        }
    }

    public async Task<bool> DeleteAsync(ApplicationUserFriendship entity, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"{nameof(DeleteAsync)} starts");
            var entry = db.Set<ApplicationUserFriendship>().Remove(entity);

            _ = await uow.SaveChangesAsync(cancellationToken);

            return true;

        }
        catch (Exception e)
        {
            logger.LogError(e, "DeleteAsync Has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(DeleteAsync)} finishes");
        }
    }

    public IQueryable<ApplicationUserFriendship> Query()
    {
        try
        {
            logger.LogInformation($"{nameof(Query)} starts");
            return db.UserFriendships.AsQueryable().AsNoTracking();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Query Has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(Query)} finishes");
        }
    }

    public async Task<ApplicationUserFriendship> UpdateAsync(ApplicationUserFriendship entity, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"{nameof(UpdateAsync)} starts");
            var entry = db.Set<ApplicationUserFriendship>().Update(entity);
            if (entry.State != EntityState.Modified)
            {
                throw new RepositoryException("Entity could not be updated on the context.");
            }
            var result = await uow.SaveChangesAsync(cancellationToken);
            if (result <= 0)
            {
                throw new RepositoryException("No changes were saved to the database.");
            }
            return entry.Entity;
        }
        catch (Exception e)
        {
            logger.LogError(e , "UpdateAsync Has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(UpdateAsync)} finishes");
        }
    }
}
