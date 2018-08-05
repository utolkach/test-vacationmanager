using System;

namespace VacationManager.ApplicationServices.Models
{
    public class EmployeeModel : EntityModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Position { get; set; }
        public Guid PositionId { get; set; }
    }
}