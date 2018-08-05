using System;
using System.Collections.Generic;
using System.Linq;
using VacationManager.ApplicationServices.Models;
using VacationManager.ApplicationServices.Services;
using VacationManager.DomainServices.Entities;
using VacationManager.Tests.FakeRepositories;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace VacationManager.Tests.Services.VacationSeviceTests
{
    public class VacationServiceTests : BaseServiceTests<VacationServiceTester, VacationService>
    {

        private readonly Guid _employeeId = Guid.NewGuid();
        private readonly Guid _employeeId2 = Guid.NewGuid();
        private readonly Guid _employeeId3 = Guid.NewGuid();
        private readonly Guid _positionId = Guid.NewGuid();
        private readonly Guid _pastVacationId = Guid.NewGuid();
        private readonly Guid _futureVacationId = Guid.NewGuid();

        public VacationServiceTests()
        {
            (_tester.FakeUnitOfWork.VacationRepository as FakeVacationRepository).MockTestData(GetVacationTestData().AsQueryable());
            (_tester.FakeUnitOfWork.EmployeeRepository as FakeEmployeeRepository).MockTestData(GetEmployeesTestData().AsQueryable());
        }

        public static List<object[]> GetBadRequestTestData => new List<object[]>
        {
            // first number needed just for easier identification of test data line
            new object[] {1, -28, -14, "You can't add vacations for past dates"},
            new object[] {2, 28, 14, "Start can't be later then End"},
            new object[] {3, 14, 15, "Minimum length is"},
            new object[] {4, 1, 50, "Vacation can't be longer than"},
            new object[] {5, 1, 7, "employee already has a vacation in this period"},
            new object[] {6, 60, 65, "Vacation limit exceeded"},
            new object[] {7, 7, 9, "You can start new vacation only after"},
            new object[] {8, 13, 16, "employees with same position can get"},
        };

        [Fact]
        public void CreateVacation_NormalFlow_VacationCreated()
        {
            var vacation = new VacationModel()
            {
                EmployeeId = _employeeId,
                Start = DateTime.Today.AddDays(370),
                End = DateTime.Today.AddDays(372)
            };

            var result = _service.Create(vacation);

            Assert.IsFalse(result.BadRequest, result.GetMessage());
            Assert.IsFalse(result.NotFound);
            Assert.AreEqual(result.Result.Start, vacation.Start);
            Assert.AreEqual(result.Result.End, vacation.End);
            Assert.AreEqual(result.Result.EmployeeId, vacation.EmployeeId);
        }

        [Theory]
        [MemberData(nameof(GetBadRequestTestData))]
        public void CreateVacation_PastDate_ErrorMessage(int n, int startOffset, int endOffset, string errorMessage)
        {
            var vacation = new VacationModel()
            {
                EmployeeId = _employeeId,
                Start = DateTime.Today.AddDays(startOffset),
                End = DateTime.Today.AddDays(endOffset)
            };

            var result = _service.Create(vacation);

            Assert.IsTrue(result.BadRequest);
            Assert.AreEqual(n, n);
            Assert.IsTrue(result.GetMessage().ToLower().Contains(errorMessage.ToLower()));
        }

        [Fact]
        public void EditVacation_VacationInFuture_VacationEdited()
        {
            var newVacationModel = new VacationModel()
            {
                Id = _futureVacationId,
                Start = DateTime.Today.AddDays(8),
                End = DateTime.Today.AddDays(11),
                EmployeeId = _employeeId2
            };
            var result = _service.Edit(newVacationModel);

            Assert.IsFalse(result.BadRequest);
            Assert.AreEqual(_futureVacationId, result.Result.Id);
        }

        [Fact]
        public void EditVacation_VacationInPast_ErrorMessage()
        {
            var newVacationModel = new VacationModel()
            {
                Id = _pastVacationId,
                Start = DateTime.Today.AddDays(8),
                End = DateTime.Today.AddDays(11),
                EmployeeId = _employeeId2
            };
            var result = _service.Edit(newVacationModel);

            Assert.IsTrue(result.BadRequest);
            Assert.IsTrue(result.GetMessage().ToLower().Contains("You can't edit past or ongoing vacations".ToLower()));
        }

        [Fact]
        public void DeleteVacation_VacationInFuture_VacationEdited()
        {
            var result = _service.Delete(_futureVacationId);
            Assert.IsFalse(result.BadRequest);
            Assert.AreEqual(_futureVacationId, result.Result.Id);
        }

        [Fact]
        public void DeleteVacation_VacationInPast_ErrorMessage()
        {
            var result = _service.Delete(_pastVacationId);

            Assert.IsTrue(result.BadRequest);
            Assert.IsTrue(result.GetMessage().ToLower().Contains("You can't delete past or ongoing vacations".ToLower()));
        }

        private List<Vacation> GetVacationTestData()
        {
            return new List<Vacation>()
            {
                new Vacation()
                {
                    Id = _pastVacationId,
                    EmployeeId = _employeeId2,
                    Start = DateTime.Today.AddDays(-20),
                    End = DateTime.Today.AddDays(-10)

                },
                new Vacation()
                {
                    Id = _futureVacationId,
                    EmployeeId = _employeeId,
                    Start = DateTime.Today.AddDays(3),
                    End = DateTime.Today.AddDays(6)
                },
                new Vacation()
                {
                    EmployeeId = _employeeId,
                    Start = DateTime.Today.AddDays(15),
                    End = DateTime.Today.AddDays(15 + 16)
                },
                new Vacation()
                {
                    EmployeeId = _employeeId2,
                    Start = DateTime.Today.AddDays(13),
                    End = DateTime.Today.AddDays(16)

                }
            };
        }

        private List<Employee> GetEmployeesTestData()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    Id = _employeeId,
                    FirstName = "John",
                    LastName = "Doe",
                    PositionId = _positionId,
                    Vacations = new List<Vacation>()
                },
                new Employee()
                {
                    Id = _employeeId2,
                    FirstName = "John1",
                    LastName = "Doe1",
                    PositionId = _positionId,
                    Vacations = new List<Vacation>()
                    {
                        new Vacation()
                        {
                            EmployeeId = _employeeId2,
                            Start = DateTime.Today.AddDays(13),
                            End = DateTime.Today.AddDays(16)
                        }
                    }
                },
                new Employee()
                {
                    Id = _employeeId3,
                    FirstName = "John2",
                    LastName = "Doe3",
                    PositionId = _positionId,
                    Vacations = new List<Vacation>()
                }
            };
        }
    }
}
