using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class MedicalApplication
    {
        public int Id { get; set; }

        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public string PassportSeries { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;

        [NotMapped]
        public decimal Price { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public ApplicationProcessStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
