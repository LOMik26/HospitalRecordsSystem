using Hospital.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Hospital.Domain.Entities
{
    public class AppointmentDetails
    {
        public int Id { get; set; }

        // Связь один-к-одному с Appointment
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = null!;

        public bool IsCame { get; set; }
        public SickLeaveStatus SickLeaveStatus { get; set; }
        public string? Symptoms { get; set; }
        public string? Complaints { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Medications { get; set; }
        public string? Operations { get; set; }
        public string? Comment { get; set; }
    }
}
