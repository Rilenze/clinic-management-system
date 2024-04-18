using System.ComponentModel.DataAnnotations;

namespace clinic_management_system.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
