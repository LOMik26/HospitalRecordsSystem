using Hospital.Data;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Hospital.UI.ViewModels
{
    public class MedicalApplicationManagementViewModel : BaseViewModel
    {
        private readonly HospitalDbContext _context = new();

        public ObservableCollection<MedicalApplication> Applications { get; set; } = new();

        private MedicalApplication? _selectedApplication;
        public MedicalApplication? SelectedApplication
        {
            get => _selectedApplication;
            set
            {
                _selectedApplication = value;
                OnPropertyChanged();
            }
        }

        public ICommand ApproveCommand { get; }
        public ICommand RejectCommand { get; }

        public MedicalApplicationManagementViewModel()
        {
            LoadApplications();

            ApproveCommand = new RelayCommand(Approve, CanProcess);
            RejectCommand = new RelayCommand(Reject, CanProcess);
        }

        private void LoadApplications()
        {
            Applications = new ObservableCollection<MedicalApplication>(
                _context.Applications
                        .Where(a => a.Status == ApplicationProcessStatus.Pending)
                        .ToList()
            );
        }

        private bool CanProcess(object? obj)
        {
            return SelectedApplication != null;
        }

        private void Approve(object? obj)
        {
            if (SelectedApplication == null) return;

            SelectedApplication.Status = ApplicationProcessStatus.Accepted;

            _context.SaveChanges();

            Applications.Remove(SelectedApplication);
        }

        private void Reject(object? obj)
        {
            if (SelectedApplication == null) return;

            SelectedApplication.Status = ApplicationProcessStatus.Rejected;

            _context.SaveChanges();

            Applications.Remove(SelectedApplication);
        }
    }
}
