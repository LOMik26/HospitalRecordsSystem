using Hospital.Data;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace Hospital.UI.Views
{
    public partial class AppointmentListWindow : Window
    {
        private readonly HospitalDbContext _context = new();
        private readonly int _patientId;
        private readonly bool _isEmployeeView;

        /// <param name="patientId">ID пациента, чьи приёмы отображаются.</param>
        /// <param name="isEmployeeView">true — сотрудник (можно добавлять), false — пациент (только просмотр).</param>
        public AppointmentListWindow(int patientId, bool isEmployeeView = false)
        {
            InitializeComponent();
            _patientId = patientId;
            _isEmployeeView = isEmployeeView;

            // Пациент может только просматривать приёмы, кнопка «Добавить» — только для сотрудника
            if (!isEmployeeView)
                AddButton.Visibility = Visibility.Collapsed;

            LoadAppointments();
        }

        private void LoadAppointments()
        {
            AppointmentsGrid.ItemsSource = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Details)
                .Where(a => a.PatientId == _patientId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new AppointmentEditWindow(_patientId);
            editWindow.Owner = this;
            if (editWindow.ShowDialog() == true)
                LoadAppointments();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsGrid.SelectedItem is not Appointment appointment)
            {
                MessageBox.Show("Выберите приём для редактирования.");
                return;
            }

            var editWindow = new AppointmentEditWindow(_patientId, appointment.Id);
            editWindow.Owner = this;
            if (editWindow.ShowDialog() == true)
                LoadAppointments();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
