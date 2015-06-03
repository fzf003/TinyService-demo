using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;

namespace TinyService.Domain.Repository
{
    public abstract class AbstractRepository<TId, TEntity> : IRepository<TId, TEntity>
         where TEntity : class,IEntity<TId>
    {
        public abstract IQueryable<TEntity> GetAll();

        public virtual int Count()
        {
            return this.GetAll().Count();
        }

        public virtual int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Count(predicate);
        }


        public virtual TEntity FirstOrDefault(TId id)
        {
            return this.GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public virtual TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().FirstOrDefault(predicate);
        }

        public abstract  TEntity Insert(TEntity entity);


        public abstract TEntity Update(TEntity entity);


        public virtual TEntity Update(TId id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        public abstract void Delete(TEntity entity);

        public abstract void Delete(TId id);


        public virtual void Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            GetAll().Where(predicate).ToList().ForEach(entity=>{
             Delete(entity);
            });
        }

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return EqualityComparer<TId>.Default.Equals(entity.ID, default(TId))
                ? Insert(entity)
                : Update(entity);
        }

        public virtual TEntity Get(TId id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new Exception("没有找到该对象：" + typeof(TEntity).FullName + ", ID: " + id);
            }

            return entity;
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TId id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "ID"),
                Expression.Constant(id, typeof(TId))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }


       
    }
}
