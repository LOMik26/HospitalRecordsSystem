using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Enums;
namespace Hospital.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        // Пациент, на которого назначен приём
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }

        // Детали приёма (медицинские записи)
        public AppointmentDetails? Details { get; set; }
    }
}
