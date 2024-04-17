namespace clinic_management_system.Models
{
    public enum Title {
        specialist,
        resident,
        nurse
    }
    public class Doctor
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public Title title { get; set; }
        public string code { get; set; }
    }
}
