using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.UI.Views
{
    public partial class EmployeeMainWindow : Window
    {
        public EmployeeMainWindow()
        {
            InitializeComponent();
        }

        private void Applications_Click(object sender, RoutedEventArgs e)
        {
            var window = new EmployeeApplicationsWindow();
            window.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаемся на окно авторизации
            var login = new MainWindow();
            login.Show();
            Close();
        }

    }
}
