using VacationManager.DomainServices.Context;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.Repositories.Interfaces;

namespace VacationManager.DomainServices.Repositories
{
    public class VacationRepository : BaseRepository<Vacation>, IVacationRepository
    {
        public VacationRepository(IDatabaseContext context)
        {
            base.context = context;
        }
    }
}