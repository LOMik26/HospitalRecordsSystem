using Hospital.Data;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Hospital.UI.ViewModels
{
    public class RegisterViewModel
    {
        private readonly HospitalDbContext _context = new();

        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        public string FullName { get; set; } = "";
        public string Phone { get; set; } = "";
        public DateTime BirthDate { get; set; } = DateTime.Today;
        public string PassportSeries { get; set; } = "";
        public string PassportNumber { get; set; } = "";

        public bool Register()
        {
            if (_context.Users.Any(u => u.Login == Login))
            {
                MessageBox.Show("Логин уже существует");
                return false;
            }

            var patient = new Patient
            {
                FullName = FullName,
                Phone = Phone,
                BirthDate = BirthDate,
                PassportSeries = PassportSeries,
                PassportNumber = PassportNumber
            };

            _context.Patients.Add(patient);
            _context.SaveChanges();

            var user = new User
            {
                Login = Login,
                PasswordHash = HashPassword(Password),
                Role = UserRole.Patient,
                PatientId = patient.Id
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
