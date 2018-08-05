using VacationManager.ApplicationServices.Processors;
using VacationManager.ApplicationServices.Services;

namespace VacationManager.Tests.Services.VacationSeviceTests
{
    public class VacationServiceTester : BaseServiceTester<VacationService>
    {
        public override VacationService Build()
        {
            return new VacationService(new VacationProcessor(FakeUnitOfWork, Mapper), Mapper, FakeUnitOfWork);
        }
    }
}
