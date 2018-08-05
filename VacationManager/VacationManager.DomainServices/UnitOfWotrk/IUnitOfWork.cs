using VacationManager.DomainServices.Repositories.Interfaces;

namespace VacationManager.DomainServices.UnitOfWotrk
{
    public interface IUnitOfWork
    {
        IVacationRepository VacationRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IPositionRepository PositionRepository { get; }
        void Save();
    }
}
