using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace clinic_management_system.Models
{

    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Birth date")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        public string? Address { get; set; }
        [DisplayName("Phone number")]
        public string? PhoneNumber { get; set; }
    }
}
