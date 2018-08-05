using System;
using VacationManager.DomainServices.Entities;
using Xunit;

namespace VacationManager.Tests.Entities
{
    public class VacationEntityTests
    {
        [Fact]
        public void GetLength()
        {
            var delta = 365;
            var length = delta + 1;
            var vacation = new Vacation()
            {
                Start = DateTime.Today,
                End = DateTime.Today.AddDays(delta)
            };

            Assert.Equal(length, vacation.GetLength());
        }

        [Fact]
        public void GetYearDays()
        {
            var vacation = new Vacation()
            {
                Start = new DateTime(2018, 1, 1),
                End = new DateTime(2019, 1, 1),
            };

            Assert.Equal(1, vacation.GetDaysForYear(2019));
            Assert.Equal(365, vacation.GetDaysForYear(2018));
        }
    }
}
