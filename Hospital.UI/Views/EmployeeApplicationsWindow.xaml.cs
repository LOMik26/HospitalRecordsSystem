using Hospital.Data;
using Hospital.Domain.Enums;
using System.Linq;
using System.Windows;
namespace Hospital.UI.Views
{
    public partial class EmployeeApplicationsWindow : Window
    {
        private readonly HospitalDbContext _context = new();

        private Domain.Entities.User? _currentUser;

        public EmployeeApplicationsWindow()
        {
            InitializeComponent();
            LoadApplications();
        }

        public void SetCurrentUser(Domain.Entities.User user)
        {
            _currentUser = user;
        }

        private void LoadApplications()
        {
            // Показываем оплаченные заявки, кроме отклонённых — чтобы принятые заявки не исчезали сразу
            ApplicationsGrid.ItemsSource = _context.Applications
                .Where(a => a.PaymentStatus == PaymentStatus.Paid
                         && a.Status != ApplicationProcessStatus.Rejected)
                .ToList();
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationsGrid.SelectedItem is not Domain.Entities.MedicalApplication app)
                return;

            // Если у заявки нет связанного пациента — пытаемся найти или создать запись пациента
            if (app.PatientId == 0)
            {
                var existing = _context.Patients.FirstOrDefault(p => p.PassportSeries == app.PassportSeries && p.PassportNumber == app.PassportNumber);
                if (existing == null)
                {
                    var newPatient = new Domain.Entities.Patient
                    {
                        FullName = app.FullName,
                        Phone = app.Phone,
                        BirthDate = app.BirthDate,
                        PassportSeries = app.PassportSeries,
                        PassportNumber = app.PassportNumber
                    };
                    _context.Patients.Add(newPatient);
                    _context.SaveChanges();
                    app.PatientId = newPatient.Id;
                }
                else
                {
                    app.PatientId = existing.Id;
                }
            }

            app.Status = ApplicationProcessStatus.Accepted;

            // Создаём приём — по умолчанию назначаем на следующий день в 09:00
            var appointment = new Domain.Entities.Appointment
            {
                PatientId = app.PatientId,
                AppointmentDate = System.DateTime.Now.Date.AddDays(1).AddHours(9)
            };
            _context.Appointments.Add(appointment);

            _context.SaveChanges();

            MessageBox.Show("Заявка принята и создан приём: " + appointment.AppointmentDate.ToString());
            LoadApplications();
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationsGrid.SelectedItem is not Domain.Entities.MedicalApplication app)
                return;

            app.Status = ApplicationProcessStatus.Rejected;
            _context.SaveChanges();

            MessageBox.Show("Заявка отклонена");
            LoadApplications();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
