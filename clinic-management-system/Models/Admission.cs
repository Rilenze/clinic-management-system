namespace clinic_management_system.Models
{
    public class Admission
    {
        public DateTime AdmissionDate { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public bool Urgency { get; set; }
    }
}
