using Hospital.Data;
using Hospital.Domain.Enums;
using Hospital.UI.Views;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Hospital.Domain.Entities;
namespace Hospital.UI.ViewModels
{
    public class LoginViewModel
    {
        private readonly HospitalDbContext _context = new();

        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        private void ExecuteLogin(Window loginWindow)
        {
            EnsureAdminExists();

            var user = _context.Users.FirstOrDefault(u => u.Login == Login);
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }

            var hash = HashPassword(Password);
            if (user.PasswordHash != hash)
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

            Window nextWindow;
            if (user.Role == UserRole.Patient)
            {
                var patient = _context.Patients.FirstOrDefault(p => p.Id == user.Id);
                if (patient == null)
                {
                    MessageBox.Show("У этого аккаунта нет связанной записи пациента. Обратитесь к администратору.");
                    return;
                }
                nextWindow = new PatientMainWindow(patient.Id);
            }
            else if (user.Role == UserRole.Employee)
                nextWindow = new EmployeeMainWindow();
            else
                return;

            nextWindow.Show();
            loginWindow.Close();
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public void LoginUser(Window loginWindow)
        {
            ExecuteLogin(loginWindow);
        }

        private void EnsureAdminExists()
        {
            if (_context.Users.Any(u => u.Role == UserRole.Employee))
                return;

            var admin = new User
            {
                Login = "admin",
                PasswordHash = HashPassword("admin"),
                Role = UserRole.Employee
            };

            _context.Users.Add(admin);
            _context.SaveChanges();
        }

    }
}
