using Hospital.Data;
using Hospital.Domain.Enums;
using Hospital.UI.Enums;
using System;
using System.Windows;

namespace Hospital.UI.Views
{
    public partial class PaymentWindow : Window
    {
        private readonly int _applicationId;

        public PaymentWindow(int applicationId)
        {
            InitializeComponent();
            _applicationId = applicationId;
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            using var context = new HospitalDbContext();
            var app = context.Applications.Find(_applicationId);

            if (app == null) return;

            app.PaymentStatus = PaymentStatus.Paid;
            context.SaveChanges();

            MessageBox.Show("Оплата успешно выполнена (имитация)");
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
