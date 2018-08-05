using System;

namespace VacationManager.ApplicationServices.Models
{
    public class VacationModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int GetLength()
        {
            return (this.End - this.Start).Days + 1;
        }
    }
}
