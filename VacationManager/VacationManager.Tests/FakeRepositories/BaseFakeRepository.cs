using System.Data.Entity;
using System.Linq;
using NSubstitute;
using VacationManager.DomainServices.Context;
using VacationManager.DomainServices.Entities;
using VacationManager.DomainServices.Repositories;

namespace VacationManager.Tests.FakeRepositories
{
    public class BaseFakeRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public void MockTestData(IQueryable<TEntity> entities)
        {
            IDbSet<TEntity> mockDbSet = Substitute.For<IDbSet<TEntity>>();
            mockDbSet.Provider.Returns(entities.Provider);
            mockDbSet.Expression.Returns(entities.Expression);
            mockDbSet.ElementType.Returns(entities.ElementType);
            mockDbSet.GetEnumerator().Returns(entities.GetEnumerator());

            this.context = Substitute.For<IDatabaseContext>();
            this.context.AddOrUpdate<TEntity>(Arg.Any<TEntity>()).Returns(callinfo => callinfo.ArgAt<TEntity>(0));
            this.context.GetSet<TEntity>().Returns(mockDbSet);
        }
    }
}
