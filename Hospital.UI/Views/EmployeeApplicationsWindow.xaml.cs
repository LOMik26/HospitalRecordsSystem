using Hospital.Data;
using Hospital.Domain.Enums;
using System.Linq;
using System.Windows;
namespace Hospital.UI.Views
{
    public partial class EmployeeApplicationsWindow : Window
    {
        private readonly HospitalDbContext _context = new();

        public EmployeeApplicationsWindow()
        {
            InitializeComponent();
            LoadApplications();
        }

        private void LoadApplications()
        {
            ApplicationsGrid.ItemsSource = _context.Applications
                .Where(a => a.PaymentStatus == PaymentStatus.Paid
                         && a.Status == ApplicationProcessStatus.Pending)
                .ToList();
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationsGrid.SelectedItem is not Domain.Entities.MedicalApplication app)
                return;

            app.Status = ApplicationProcessStatus.Accepted;
            _context.SaveChanges();

            MessageBox.Show("Заявка принята");
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
