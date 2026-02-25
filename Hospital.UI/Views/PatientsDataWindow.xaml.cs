using Hospital.Data;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace Hospital.UI.Views
{
    public partial class PatientsDataWindow : Window
    {
        private readonly HospitalDbContext _context = new();

        public PatientsDataWindow()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients(string? filter = null)
        {
            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var f = filter.ToLower();
                query = query.Where(p =>
                    p.FullName.ToLower().Contains(f) ||
                    p.Phone.ToLower().Contains(f) ||
                    p.PassportSeries.ToLower().Contains(f) ||
                    p.PassportNumber.ToLower().Contains(f) ||
                    p.BirthDate.ToString().Contains(f));
            }

            PatientsGrid.ItemsSource = query.ToList();
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            LoadPatients(SearchBox.Text);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsGrid.SelectedItem is not Patient patient)
            {
                MessageBox.Show("Выберите пациента для редактирования.");
                return;
            }

            var editWindow = new PatientEditWindow(patient.Id);
            editWindow.Owner = this;
            if (editWindow.ShowDialog() == true)
                LoadPatients(SearchBox.Text);
        }

        private void OpenAppointments_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsGrid.SelectedItem is not Patient patient)
            {
                MessageBox.Show("Выберите пациента для просмотра приёмов.");
                return;
            }

            var window = new AppointmentListWindow(patient.Id, isEmployeeView: true);
            window.Owner = this;
            window.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsGrid.SelectedItem is not Patient patient)
            {
                MessageBox.Show("Выберите пациента для удаления.");
                return;
            }

            var result = MessageBox.Show(
                $"Удалить пациента «{patient.FullName}»? Все его приёмы и заявки также будут удалены.",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                var tracked = _context.Patients
                    .Include(p => p.Appointments)
                        .ThenInclude(a => a.Details)
                    .Include(p => p.Applications)
                    .FirstOrDefault(p => p.Id == patient.Id);

                if (tracked == null) return;

                foreach (var appt in tracked.Appointments)
                {
                    if (appt.Details != null)
                        _context.AppointmentDetails.Remove(appt.Details);
                }
                _context.Appointments.RemoveRange(tracked.Appointments);
                _context.Applications.RemoveRange(tracked.Applications);
                _context.Patients.Remove(tracked);
                _context.SaveChanges();

                LoadPatients(SearchBox.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
