using Hospital.Data;
using Hospital.Domain.Enums;
using System;
using System.Windows;

namespace Hospital.UI.Views
{
    public partial class PaymentWindow : Window
    {
        private readonly int _applicationId;

        public PaymentWindow()
        {
            InitializeComponent();
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            // Здесь имитируем оплату — в учебном проекте без реальной интеграции
            MessageBox.Show("Оплата успешно выполнена (имитация)");
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
