using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;
using TinyService.Domain.Repository;

namespace TinyService.Extension.Repository
{

   
    public static class OnlyWriteRepositoryExtenion
    {
        public static Task<TEntity> InsertAsync<TId, TEntity>(this IOnlyWriteRepository<TId, TEntity> repository, TEntity entity) where TEntity : class,IEntity<TId>
        {
            return Task.FromResult<TEntity>(repository.Insert(entity));
        }


        public static Task<TEntity> UpdateAsync<TId, TEntity>(this IOnlyWriteRepository<TId, TEntity> repository, TEntity entity) where TEntity : class,IEntity<TId>
        {
            return Task.FromResult<TEntity>(repository.Update(entity));
        }



        public static Task DeleteAsync<TId, TEntity>(this IOnlyWriteRepository<TId, TEntity> repository,TEntity Entity) where TEntity : class,IEntity<TId>
        {
             return  Task.Run(()=> repository.Delete(Entity));
        }

        public static Task DeleteAsync<TId, TEntity>(this IOnlyWriteRepository<TId, TEntity> repository, TId id) where TEntity : class,IEntity<TId>
        {
            return Task.Run(() => repository.Delete(id));
        }

        public static Task DeleteAsync<TId, TEntity>(this IOnlyWriteRepository<TId, TEntity> repository, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate) where TEntity : class,IEntity<TId>
        {
            return Task.Run(() => repository.Delete(predicate));
        }


    }
}
