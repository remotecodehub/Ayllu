using Ayllu.Web.Domain.Abstractions.Common;
using System.Linq.Expressions;

namespace Ayllu.Web.Application.Common.Abstractions.Storage;

public interface IRepository<T>
    where T : class, IEntity<string>
{
    IQueryable<T> Query();
    Task<T> CreateAsync(T entity, CancellationToken ct);
    Task<T> UpdateAsync(T entity, CancellationToken ct);
    Task<bool> DeleteAsync(T entity, CancellationToken ct);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);
}