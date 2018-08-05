using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models
{
    public class UserApiContract
    {
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}