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
            _vm.LoadPatient(_patientId);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Открываем окно оплаты перед созданием заявки
                var payWindow = new PaymentWindow();
                payWindow.Owner = this;
                var paid = payWindow.ShowDialog();
                if (paid == true)
                {
                    _vm.CreateApplication(_patientId, paid: true);
                    MessageBox.Show("Заявка успешно создана и оплачена.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Оплата не выполнена. Заявка не создана.");
                }
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
