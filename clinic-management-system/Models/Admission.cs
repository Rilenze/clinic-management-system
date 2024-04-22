using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic_management_system.Models
{
    public class Admission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Admission date and time")]
        public DateTime AdmissionDateTime { get; set; }
        [DisplayName("Patient")]
        public int PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public Patient? Patient { get; set; }
        [DisplayName("The competent doctor")]
        public int DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))]
        public Doctor? Doctor { get; set; }
        public bool Urgency { get; set; }
    }
}
