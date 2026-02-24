using Hospital.Data;
using System.Linq;
using System.Windows;

namespace Hospital.UI.Views
{
    public partial class UsersListWindow : Window
    {
        public UsersListWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using var ctx = new HospitalDbContext();
                UsersGrid.ItemsSource = ctx.Users.Select(u => new
                {
                    u.Id,
                    u.Login,
                    u.PasswordHash,
                    Role = u.Role.ToString(),
                    u.PatientId
                }).ToList();
            }
            catch (System.Exception ex)
            {
                // Если загрузка пользователей не удалась, показываем сообщение и оставляем грид пустым
                MessageBox.Show("Не удалось загрузить пользователей: " + (ex.InnerException?.Message ?? ex.Message));
                UsersGrid.ItemsSource = System.Array.Empty<object>();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}