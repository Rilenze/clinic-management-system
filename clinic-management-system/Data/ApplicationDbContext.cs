using clinic_management_system.Models;
using Microsoft.EntityFrameworkCore;

namespace clinic_management_system.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {    
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<MedicalReport> MedicalReports { get; set; }


    }
}
