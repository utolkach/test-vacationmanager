using VacationManager.DomainServices.Repositories.Interfaces;
using VacationManager.DomainServices.UnitOfWotrk;

namespace VacationManager.Tests.FakeRepositories
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public IVacationRepository VacationRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IPositionRepository PositionRepository { get; }

        public FakeUnitOfWork()
        {
            this.VacationRepository = new FakeVacationRepository();
            this.EmployeeRepository = new FakeEmployeeRepository();
            this.PositionRepository = new FakePositionRepository();
        }

        public void Save()
        {

        }
    }
}
