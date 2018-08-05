using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VacationManager.DomainServices.Entities;

namespace VacationManager.DomainServices.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Create(TEntity entity);

        IEnumerable<TEntity> Get();

        TEntity Get(Guid key);

        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> filter);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        void SaveChanges();
    }
}
