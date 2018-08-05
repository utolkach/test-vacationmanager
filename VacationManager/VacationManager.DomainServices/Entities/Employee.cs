using System;
using System.Collections.Generic;

namespace VacationManager.DomainServices.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public Position Position { get; set; }
        public Guid PositionId { get; set; }

        public virtual ICollection<Vacation> Vacations { get; set; }
    }
}