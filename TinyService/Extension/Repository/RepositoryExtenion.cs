﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;
using TinyService.Domain.Repository;

namespace TinyService.Extension.Repository
{
    public static class RepositoryExtenion
    {
        public static async Task<TEntity> UpdateAsync<TId, TEntity>(this IRepository<TId, TEntity> repository, TId id, Func<TEntity, Task> updateAction) where TEntity : class,IEntity<TId>
        {
            var entity = repository.Get(id);
            await updateAction(entity);
            return entity;
        }

        public static async Task<TEntity> InsertOrUpdateAsync<TId, TEntity>(this IRepository<TId, TEntity> repository, TEntity entity) where TEntity : class,IEntity<TId>
        {
            return await Task.Run<TEntity>(() => repository.InsertOrUpdate(entity));

        }
    }
}