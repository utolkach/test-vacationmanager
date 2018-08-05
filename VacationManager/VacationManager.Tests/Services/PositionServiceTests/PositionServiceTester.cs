using VacationManager.ApplicationServices.Services;

namespace VacationManager.Tests.Services.PositionServiceTests
{
    public class PositionServiceTester : BaseServiceTester<PositionService>
    {
        public override PositionService Build()
        {
            return new PositionService(Mapper, FakeUnitOfWork);
        }
    }
}
