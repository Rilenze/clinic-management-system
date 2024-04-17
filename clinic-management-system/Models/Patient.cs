namespace clinic_management_system.Models
{
    public enum Gender
    {
        man,
        female,
        unknown
    }
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
