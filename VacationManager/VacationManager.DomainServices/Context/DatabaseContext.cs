using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.Mapping;

namespace VacationManager.DomainServices.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString)
        {
        }

        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Vacation> Vacations { get; set; }
        public IDbSet<Position> Positions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeMapping());
            modelBuilder.Configurations.Add(new VacationMapping());
            modelBuilder.Configurations.Add(new PositionMapping());

            base.OnModelCreating(modelBuilder);
        }

        public new void SaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName,
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public IDbSet<TEntity> GetSet<TEntity>() where TEntity : BaseEntity
        {
            return Set<TEntity>();
        }

        public TEntity AddOrUpdate<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            this.GetSet<TEntity>().AddOrUpdate(item);
            return item;
        }
    }
}
