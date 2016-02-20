using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;

namespace TinyService.Domain.Repository
{
    public interface IRepository<TId, TEntity> : IOnlyReadRepository<TId, TEntity>, IOnlyWriteRepository<TId, TEntity>
         where TEntity : class,IAggregateRoot
    {

        int Count();

        int Count(Expression<Func<TEntity, bool>> predicate);
         
    }

    public interface IOnlyReadRepository<TId, TEntity>
         where TEntity : class,IAggregateRoot
    {
         IQueryable<TEntity> GetAll();
         TEntity FirstOrDefault(TId id);
         TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
         TEntity Get(TId id);
    }

    public interface IOnlyWriteRepository<TId,TEntity>
        where TEntity : class,IAggregateRoot
    {
         TEntity Insert(TEntity entity);

         TEntity Update(TEntity entity);

         TEntity Update(TId id, Action<TEntity> updateAction);

         TEntity InsertOrUpdate(TEntity entity);

         void Delete(TEntity entity);

         void Delete(Expression<Func<TEntity, bool>> predicate);

         void Delete(TId id);

    }
}
