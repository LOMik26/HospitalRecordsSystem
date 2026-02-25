using Hospital.Data;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.UI.Views
{
    public partial class AppointmentEditWindow : Window
    {
        private readonly HospitalDbContext _context = new();
        private readonly int _patientId;
        private readonly int? _appointmentId;

        /// <summary>Создание нового приёма.</summary>
        public AppointmentEditWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            _appointmentId = null;
            TitleBlock.Text = "Добавление приёма";
            MedicalFieldsPanel.IsEnabled = false;
            SickLeaveCombo.SelectedIndex = 0;
        }

        /// <summary>Редактирование существующего приёма.</summary>
        public AppointmentEditWindow(int patientId, int appointmentId)
        {
            InitializeComponent();
            _patientId = patientId;
            _appointmentId = appointmentId;
            TitleBlock.Text = "Редактирование приёма";
            SickLeaveCombo.SelectedIndex = 0;
            LoadAppointment();
        }

        private void LoadAppointment()
        {
            var appt = _context.Appointments
                .Include(a => a.Details)
                .FirstOrDefault(a => a.Id == _appointmentId);

            if (appt == null) return;

            AppointmentDatePicker.SelectedDate = appt.AppointmentDate.Date;
            AppointmentTimeBox.Text = appt.AppointmentDate.ToString("HH:mm");

            var d = appt.Details;
            if (d != null)
            {
                IsCameCheckBox.IsChecked = d.IsCame;
                SymptomsBox.Text = d.Symptoms;
                ComplaintsBox.Text = d.Complaints;
                DiagnosisBox.Text = d.Diagnosis;
                TreatmentBox.Text = d.Treatment;
                MedicationsBox.Text = d.Medications;
                OperationsBox.Text = d.Operations;
                CommentBox.Text = d.Comment;

                // Set SickLeaveStatus combo
                if (d.SickLeaveStatus == SickLeaveStatus.Issued)
                    SickLeaveCombo.SelectedIndex = 1;
                else
                    SickLeaveCombo.SelectedIndex = 0;
            }

            MedicalFieldsPanel.IsEnabled = IsCameCheckBox.IsChecked == true;
        }

        private void IsCame_Changed(object sender, RoutedEventArgs e)
        {
            if (MedicalFieldsPanel == null) return;
            MedicalFieldsPanel.IsEnabled = IsCameCheckBox.IsChecked == true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, укажите дату приёма.");
                return;
            }

            if (!System.TimeSpan.TryParse(AppointmentTimeBox.Text, out var timeSpan))
            {
                MessageBox.Show("Пожалуйста, укажите время приёма в формате ЧЧ:ММ.");
                return;
            }

            var appointmentDate = AppointmentDatePicker.SelectedDate.Value.Date + timeSpan;

            try
            {
                Appointment appt;

                if (_appointmentId == null)
                {
                    // Создаём новый приём
                    appt = new Appointment
                    {
                        PatientId = _patientId,
                        AppointmentDate = appointmentDate
                    };
                    _context.Appointments.Add(appt);
                    _context.SaveChanges();
                }
                else
                {
                    appt = _context.Appointments
                        .Include(a => a.Details)
                        .First(a => a.Id == _appointmentId);
                    appt.AppointmentDate = appointmentDate;
                }

                // Сохраняем медицинские данные (AppointmentDetails)
                var details = appt.Details ?? new AppointmentDetails { AppointmentId = appt.Id };
                bool isNew = appt.Details == null;

                details.IsCame = IsCameCheckBox.IsChecked == true;

                if (details.IsCame)
                {
                    details.Symptoms = SymptomsBox.Text.Trim();
                    details.Complaints = ComplaintsBox.Text.Trim();
                    details.Diagnosis = DiagnosisBox.Text.Trim();
                    details.Treatment = TreatmentBox.Text.Trim();
                    details.Medications = MedicationsBox.Text.Trim();
                    details.Operations = OperationsBox.Text.Trim();
                    details.SickLeaveStatus = SickLeaveCombo.SelectedIndex == 1
                        ? SickLeaveStatus.Issued
                        : SickLeaveStatus.None;
                }
                else
                {
                    details.Symptoms = null;
                    details.Complaints = null;
                    details.Diagnosis = null;
                    details.Treatment = null;
                    details.Medications = null;
                    details.Operations = null;
                    details.SickLeaveStatus = null;
                }

                details.Comment = CommentBox.Text.Trim();

                if (isNew)
                {
                    details.AppointmentId = appt.Id;
                    _context.AppointmentDetails.Add(details);
                }

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
