using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hospital.UI.ViewModels;

namespace Hospital.UI.Views
{
    public partial class ApplicationWindow : Window
    {
        private readonly ApplicationViewModel _vm = new();
        private readonly int _patientId;


        public ApplicationWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            DataContext = _vm; // если используешь привязки
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _vm.CreateApplication(_patientId);
                MessageBox.Show("Заявка успешно создана.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании заявки:\n" + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
