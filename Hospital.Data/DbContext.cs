using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalApplication> Applications { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDetails> AppointmentDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Настраиваем подключение к базе (строка подключения задаётся как нужно)
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Отношение Patient 1:N Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            // Отношение Appointment 1:1 AppointmentDetails
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Details)
                .WithOne(d => d.Appointment)
                .HasForeignKey<AppointmentDetails>(d => d.AppointmentId);

            // Конвертация enum в string для совместимости с текущими миграциями
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<AppointmentDetails>()
                .Property(d => d.SickLeaveStatus)
                .HasConversion<string?>();

            // Конвертация enum PaymentStatus и ApplicationProcessStatus в string
            modelBuilder.Entity<MedicalApplication>()
                .Property(m => m.PaymentStatus)
                .HasConversion<string>();

            modelBuilder.Entity<MedicalApplication>()
                .Property(m => m.Status)
                .HasConversion<string>();
        }
    }
}
