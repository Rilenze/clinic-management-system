namespace clinic_management_system.Models
{
    public enum Title 
    {
        specialist,
        resident,
        nurse
    }
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Title Title { get; set; }
        public string Code { get; set; }
    }
}
