using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic_management_system.Models
{
    public class MedicalReport
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ReportDescription { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("Admission")]
        public int AdmissionId { get; set; }
    }
}
