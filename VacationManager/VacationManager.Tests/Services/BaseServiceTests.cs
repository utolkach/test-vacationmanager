namespace VacationManager.Tests.Services
{
    public class BaseServiceTests<TServiceTester, TService>
        where TServiceTester : BaseServiceTester<TService>, new()
        where TService : class
    {
        protected TServiceTester _tester;
        protected TService _service;

        public BaseServiceTests()
        {
            _tester = new TServiceTester();
            _service = _tester.Build();
        }
    }
}