using VacationManager.ApplicationServices.Services;

namespace VacationManager.Tests.Services.EmployeeServiceTests
{
    public class EmployeeServiceTester : BaseServiceTester<EmployeeService>
    {
        public override EmployeeService Build()
        {
            return new EmployeeService(Mapper, FakeUnitOfWork);
        }
    }
}
