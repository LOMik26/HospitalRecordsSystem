using Hospital.Data;
using Hospital.Domain.Entities;
using System.Windows;

namespace Hospital.UI.Views
{
    public partial class PatientEditWindow : Window
    {
        private readonly HospitalDbContext _context = new();
        private readonly int _patientId;

        public PatientEditWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            LoadPatient();
        }

        private void LoadPatient()
        {
            var patient = _context.Patients.Find(_patientId);
            if (patient == null) return;

            FullNameBox.Text = patient.FullName;
            PhoneBox.Text = patient.Phone;
            BirthDatePicker.SelectedDate = patient.BirthDate;
            PassportSeriesBox.Text = patient.PassportSeries;
            PassportNumberBox.Text = patient.PassportNumber;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneBox.Text) ||
                BirthDatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(PassportSeriesBox.Text) ||
                string.IsNullOrWhiteSpace(PassportNumberBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                var patient = _context.Patients.Find(_patientId);
                if (patient == null) return;

                patient.FullName = FullNameBox.Text.Trim();
                patient.Phone = PhoneBox.Text.Trim();
                patient.BirthDate = BirthDatePicker.SelectedDate.Value;
                patient.PassportSeries = PassportSeriesBox.Text.Trim();
                patient.PassportNumber = PassportNumberBox.Text.Trim();

                _context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
