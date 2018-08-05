using VacationManager.DomainServices.Context;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.Repositories.Interfaces;

namespace VacationManager.DomainServices.Repositories
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(IDatabaseContext context)
        {
            base.context = context;
        }
    }
}
