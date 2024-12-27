using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Intaker.TMS.Dal.Repositories;

public class BasicRepository<TId, TEntity> : IBasicRepository<TId, TEntity>
 where TEntity : class
 where TId : struct
{
    protected readonly WorkTaskContext dbContext;

    public BasicRepository(WorkTaskContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
    {
        TEntity? entity = tracking
            ? await dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate)
            : await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity model)
    {
        dbContext.Set<TEntity>().Update(model);
        await dbContext.SaveChangesAsync();
        return model;
    }
}
