using Hospital.Data;
using Hospital.Domain.Enums;
using System;
using System.Windows;

namespace Hospital.UI.Views
{
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CardNumberBox.Text) ||
                string.IsNullOrWhiteSpace(ExpiryBox.Text) ||
                string.IsNullOrWhiteSpace(CvvBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля формы оплаты.");
                return;
            }

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
