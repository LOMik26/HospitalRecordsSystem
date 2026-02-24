using Hospital.Domain.Entities;
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

    public partial class PatientMainWindow : Window
    {
        private readonly int _patientId;

        // новый конструктор
        public PatientMainWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
        }

        // Пример: открытие окна создания заявки (замени существующий обработчик)
        private void CreateApplication_Click(object sender, RoutedEventArgs e)
        {
            var window = new ApplicationWindow(_patientId);
            window.ShowDialog();
        }
    }
}


