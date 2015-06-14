using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;
using TinyService.Domain.Repository;
using EntityFramework.Extensions;
namespace TinyService.EntityFramework
{
    public interface IEFRepository<TId, TEntity>:IRepository<TId, TEntity>,IDisposable
        where TEntity : class,IEntity<TId>
    {
        DbContext Context { get; }
    }

    public class EFRepository<TId, TEntity> : AbstractRepository<TId, TEntity>, IEFRepository<TId, TEntity>
        where TEntity : class,IEntity<TId>
    {
        private readonly DbContext _Dbcontext;
        public EFRepository(DbContext dbcontext)
        {
            this._Dbcontext = dbcontext;
        }

        public DbContext Context
        {
            get
            {
                return this._Dbcontext;
            }
        }
        public override TEntity Insert(TEntity entity)
        {
            this._Dbcontext.Set<TEntity>().Add(entity);
            return entity;
        }

        public override void Delete(TEntity entity)
        {
            this._Dbcontext.Set<TEntity>().Remove(entity);
        }

        public override void Delete(TId id)
        {
            this._Dbcontext.Set<TEntity>().Remove(this.Get(id));
            
        }

        public override IQueryable<TEntity> GetAll()
        {
            return this._Dbcontext.Set<TEntity>().AsQueryable();
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            this._Dbcontext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        void AttachIfNot(TEntity entity)
        {
            if (!this._Dbcontext.Set<TEntity>().Local.Contains(entity))
            {
                this._Dbcontext.Set<TEntity>().Attach(entity);
            }
        }

        public void Dispose()
        {
            this._Dbcontext.Dispose();
        }
    }
}
