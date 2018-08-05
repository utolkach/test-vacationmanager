using System;
using System.Collections.Generic;
using System.Linq;
using VacationManager.ApplicationServices.Models;
using VacationManager.ApplicationServices.Services;
using VacationManager.DomainServices.Entities;
using VacationManager.Tests.FakeRepositories;
using Xunit;

namespace VacationManager.Tests.Services.EmployeeServiceTests
{
    public class EmployeeServiceTests : BaseServiceTests<EmployeeServiceTester, EmployeeService>
    {
        private static readonly Guid _employeeId = Guid.NewGuid();
        private readonly Guid _positionId = Guid.NewGuid();

        public EmployeeServiceTests()
        {
            (_tester.FakeUnitOfWork.EmployeeRepository as FakeEmployeeRepository).MockTestData(GetTestEmployeeData().AsQueryable());
            (_tester.FakeUnitOfWork.PositionRepository as FakePositionRepository).MockTestData(GetTestPositions().AsQueryable());
        }

        private List<Position> GetTestPositions()
        {
            return new List<Position>()
            {
                new Position()
                {
                    Id = _positionId,
                    Title = "superhero"
                }
            };
        }

        public static List<object[]> GetKeysForSearchTest => new List<object[]>
        {
            // first number needed just for easier identification of test data line
            new object[] {1, "try"},
            new object[] {2, "to"},
            new object[] {3, "find"},
            new object[] {4, "me"},
        };

        public static List<object[]> GetKeysForAndFiterTest => new List<object[]>
        {
            // first number needed just for easier identification of test data line
            new object[] {1, "", "FirstName", false},
            new object[] {2, null, "LastName", true},
            new object[] {3, " ", "Position", false},
            new object[] {4, "me", null, null},
        };

        [Theory]
        [MemberData(nameof(GetKeysForSearchTest))]
        public void SearchEmployee_EmployeeExistins_EmployeeReturned(int n, string key)
        {
            var result = _service.Search(key);

            Assert.Contains(result, x => x.Id == _employeeId);
            Assert.Equal(n, n);
        }

        [Fact]
        public void Create_NormalFlow_CreatedEmployeeReturned()
        {
            var newEmployeeModel = new EmployeeModel()
            {
                FirstName = "John",
                LastName = "Doe",
                PositionId = _positionId
            };

            var result = _service.Create(newEmployeeModel);

            Assert.NotNull(result.Result);
            Assert.Equal(newEmployeeModel.FirstName, result.Result.FirstName);
        }

        [Theory]
        [MemberData(nameof(GetKeysForAndFiterTest))]
        public void Search_NormalFlow_ResultReturned(int n, string key, string filedname, bool? desc)
        {
            var result = _service.Search(key, filedname, desc);

            Assert.True(result.All(x => x != null));
            Assert.Equal(n, n);
        }

        private static List<Employee> GetTestEmployeeData()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    Id = _employeeId,
                    FirstName = "try",
                    MiddleName = "to",
                    LastName = "find",
                    Position = new Position() {Id = Guid.NewGuid(), Title = "me"}
                },
                new Employee()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "1number",
                    MiddleName = "1number",
                    LastName = "1number",
                    Position = new Position() {Id = Guid.NewGuid(), Title = "1number"}
                },
                new Employee()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "2number",
                    MiddleName = "2number",
                    LastName = "2number",
                    Position = new Position() {Id = Guid.NewGuid(), Title = "2number"}
                }
            };
        }


    }
}