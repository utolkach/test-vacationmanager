using AutoMapper;
using VacationManager.Tests.FakeRepositories;

namespace VacationManager.Tests.Services
{
    public abstract class BaseServiceTester<T>
    {
        public IMapper Mapper { get; } = MappingsRegistration.GetMapperConfiguration().CreateMapper();
        public FakeUnitOfWork FakeUnitOfWork { get; } = new FakeUnitOfWork();
        public abstract T Build();
    }
}