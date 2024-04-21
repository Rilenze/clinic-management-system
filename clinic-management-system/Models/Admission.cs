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
        public int PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; }
        public bool Urgency { get; set; }
    }
}
