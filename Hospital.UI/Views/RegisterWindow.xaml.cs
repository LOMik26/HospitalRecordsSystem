using Hospital.UI.ViewModels;
using System.Windows;

namespace Hospital.UI.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly RegisterViewModel _vm = new();

        public RegisterWindow()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            _vm.Password = PasswordBox.Password;

            if (_vm.Register())
            {
                MessageBox.Show("Регистрация успешна");
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
