using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Enums;
namespace Hospital.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string PassportSeries { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;

        // Навигационные свойства: все заявки и приёмы пациента
        public ICollection<MedicalApplication> Applications { get; set; } = new List<MedicalApplication>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
