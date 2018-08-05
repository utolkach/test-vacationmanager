using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.Repositories.Interfaces;

namespace VacationManager.Tests.FakeRepositories
{
    public class FakeEmployeeRepository : BaseFakeRepository<Employee>, IEmployeeRepository
    {
    }
}
