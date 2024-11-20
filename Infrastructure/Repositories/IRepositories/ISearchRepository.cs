using System.Linq.Expressions;
using Infrastructure.Custom.ResultPattern;

namespace Infrastructure.Repositories.IRepositories;

public interface ISearchRepository<M> where M : class
{
    Task<Result<IQueryable<M>>> FilteredSearch(Expression<Func<M, bool>> predicate);
}