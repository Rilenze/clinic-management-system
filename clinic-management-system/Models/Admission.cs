using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic_management_system.Models
{
    public class Admission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime AdmissionDateTime { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public bool Urgency { get; set; }
    }
}
