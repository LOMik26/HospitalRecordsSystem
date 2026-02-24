using Hospital.Data;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Hospital.UI.ViewModels;
using Hospital.UI.Views;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Hospital.UI
{
    public partial class MainWindow : Window
    {
        private readonly LoginViewModel _vm = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            _vm.Password = PasswordBox.Password;
            _vm.LoginUser(this);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var window = new RegisterWindow();
            window.ShowDialog();
        }

    }
}
