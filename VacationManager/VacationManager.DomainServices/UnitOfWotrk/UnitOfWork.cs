using VacationManager.DomainServices.Context;
using VacationManager.DomainServices.Repositories;
using VacationManager.DomainServices.Repositories.Interfaces;

namespace VacationManager.DomainServices.UnitOfWotrk
{
    public class UnitOfWork : IUnitOfWork
    {
        public IVacationRepository VacationRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IPositionRepository PositionRepository { get; }

        private readonly IDatabaseContext _context;

        public UnitOfWork()
        {
            this._context = new DatabaseContext();
            this.VacationRepository = new VacationRepository(_context);
            this.EmployeeRepository = new EmployeeRepository(_context);
            this.PositionRepository = new PositionRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}