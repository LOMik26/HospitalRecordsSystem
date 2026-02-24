using Hospital.Data;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
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

        public MedicalApplication CreateApplication(int patientId, bool paid = false)
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
                PaymentStatus = paid ? PaymentStatus.Paid : PaymentStatus.Unpaid,
                CreatedAt = DateTime.Now
            };

            _context.Applications.Add(app);
            _context.SaveChanges();

            return app;
        }

        public void LoadPatient(int patientId)
        {
            try
            {
                if (patientId <= 0) return;

                var patient = _context.Patients.Find(patientId);
                if (patient == null) return;

                FullName = patient.FullName;
                Phone = patient.Phone;
                BirthDate = patient.BirthDate;
                PassportSeries = patient.PassportSeries;
                PassportNumber = patient.PassportNumber;
            }
            catch
            {
                // ignore load errors
            }
        }
    }
}
