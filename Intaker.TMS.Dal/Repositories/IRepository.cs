using System.Linq.Expressions;
using Intaker.TMS.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Intaker.TMS.Dal.Repositories;

public interface IBasicRepository<TId, TEntity>
 where TEntity : class
 where TId : struct  {
    Task<TEntity> CreateAsync(TEntity model);
    Task<TEntity> UpdateAsync(TEntity model);
    Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
    Task DeleteAsync(TEntity entity);
}
