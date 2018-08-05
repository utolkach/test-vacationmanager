using System;

namespace VacationManager.DomainServices.Entities
{
    public class Vacation : BaseEntity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public bool IsInFuture()
        {
            return DateTime.Today < this.Start;
        }

        public int GetLength()
        {
            return (this.End - this.Start).Days + 1;
        }

        public int GetDaysForYear(int year)
        {
            if (End.Year > year && Start.Year == year)
            {
                return (new DateTime(year + 1, 1, 1) - Start).Days;
            }
            else if (Start.Year < year && End.Year == year)
            {
                return (End - new DateTime(year - 1, 12, 31)).Days;
            }
            return GetLength();
        }
    }
}