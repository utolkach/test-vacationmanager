using System.Data.Entity;
using VacationManager.DomainServices.Entities;

namespace VacationManager.DomainServices.Context
{
    public interface IDatabaseContext
    {
        IDbSet<Employee> Employees { get; set; }
        IDbSet<Vacation> Vacations { get; set; }
        IDbSet<Position> Positions { get; set; }

        void SaveChanges();
        IDbSet<TEntity>  GetSet<TEntity>() where TEntity : BaseEntity;
        TEntity AddOrUpdate<TEntity>(TEntity item) where TEntity : BaseEntity;
    }
}