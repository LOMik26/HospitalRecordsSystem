using Hospital.Data;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Hospital.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.UI.ViewModels
{
    public class ApplicationViewModel
    {
        private readonly HospitalDbContext _context = new();

        public string FullName { get; set; } = "";
        public string Phone { get; set; } = "";
        public DateTime BirthDate { get; set; } = DateTime.Today;
        public string PassportSeries { get; set; } = "";
        public string PassportNumber { get; set; } = "";

        public static decimal Price => 1500;

        public MedicalApplication CreateApplication(int patientId)
        {
            var app = new MedicalApplication
            {
                PatientId = patientId,
                FullName = FullName,
                Phone = Phone,
                BirthDate = BirthDate,
                PassportSeries = PassportSeries,
                PassportNumber = PassportNumber,
                Price = Price,
                PaymentStatus = PaymentStatus.Unpaid,
                CreatedAt = DateTime.Now
            };

            _context.Applications.Add(app);
            _context.SaveChanges();

            return app;
        }
    }
}
