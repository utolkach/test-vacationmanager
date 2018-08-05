using System;
using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models
{
    public class VacationApiContract
    {
        public Guid Id { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }
    }
}