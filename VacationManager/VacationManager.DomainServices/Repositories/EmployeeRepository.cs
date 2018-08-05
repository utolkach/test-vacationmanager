using System.Data.Entity;
using System.Linq;
using VacationManager.DomainServices.Context;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.Repositories.Interfaces;

namespace VacationManager.DomainServices.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(IDatabaseContext context)
        {
            base.context = context;
        }

        protected override IQueryable<Employee> IncludeProperties(IQueryable<Employee> queryable)
        {
            return queryable.Include(x => x.Position).Include(x => x.Vacations);
        }
    }
}
