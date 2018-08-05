using System;
using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models
{
    public class EmployeeApiContract
    {
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100)]
        [Required]
        public string MiddleName { get; set; }

        [Required]
        public Guid PositionId { get; set; }
    }
}