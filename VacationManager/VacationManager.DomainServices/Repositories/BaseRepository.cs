using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using VacationManager.DomainServices.Context;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.Repositories.Interfaces;

namespace VacationManager.DomainServices.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected IDatabaseContext context;

        protected BaseRepository()
        {
            context = new DatabaseContext();
        }

        public  BaseRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public virtual TEntity Create(TEntity entity)
        {
            return GetDbSet().Add(entity);
        }

        public IEnumerable<TEntity> Get()
        {
            return GetDbSetWithIncludedProperties().ToList();
        }


        public TEntity Get(Guid key)
        {
            return GetDbSetWithIncludedProperties().SingleOrDefault(x => x.Id == key);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> filter)
        {
            return GetDbSetWithIncludedProperties().Where(filter).ToList();
        }

        public TEntity Update(TEntity entity)
        {
            context.AddOrUpdate(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            GetDbSet().Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        protected IDbSet<TEntity> GetDbSet()
        {
            return context.GetSet<TEntity>();
        }

        private IQueryable<TEntity> GetDbSetWithIncludedProperties()
        {
            var queryable = GetDbSet().AsQueryable();
            queryable = IncludeProperties(queryable);
            return queryable;
        }


        protected virtual IQueryable<TEntity> IncludeProperties(IQueryable<TEntity> queryable)
        {
            return queryable;
        }
    }
}