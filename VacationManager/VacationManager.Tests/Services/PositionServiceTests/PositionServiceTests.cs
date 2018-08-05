using System;
using System.Collections.Generic;
using System.Linq;
using VacationManager.ApplicationServices.Services;
using VacationManager.DomainServices.Entities;
using VacationManager.Tests.FakeRepositories;
using Xunit;

namespace VacationManager.Tests.Services.PositionServiceTests
{
    public class PositionServiceTests : BaseServiceTests<PositionServiceTester, PositionService>
    {
        private readonly List<Position> _getTestPositionsData;

        public PositionServiceTests()
        {
            _getTestPositionsData = GetTestPositionsData();
            (_tester.FakeUnitOfWork.PositionRepository as FakePositionRepository).MockTestData(_getTestPositionsData.AsQueryable());
        }

        private List<Position> GetTestPositionsData()
        {
            return new List<Position>()
            {
                new Position(){Id = Guid.NewGuid(), Title = "Big Boss"},
                new Position(){Id = Guid.NewGuid(), Title = "Even Larger Boss"}
            };
        }

        [Fact]
        public void GetPositions_PositionExisting_PositionsReturned()
        {
            var result = _service.GetPositions();

            foreach (var position in result)
            {
                Assert.Contains(_getTestPositionsData, x => x.Id == position.Id);
            }
        }

        [Fact]
        public void CreatePosition_NormalFlow_PositionReturned()
        {
            var position = "position";
            var result = _service.CreatePosition(position);
            Assert.Equal(position, result.Title);
        }
    }
}
