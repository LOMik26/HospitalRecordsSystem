using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Hospital.Data;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hospital.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Ensure an admin user exists after database reset/cleanup.
            try
            {
                using var ctx = new HospitalDbContext();

                // Ensure Applications.Status column exists (add if missing) to match model
                try
                {
                    var sql = @"
IF NOT EXISTS(
    SELECT * FROM sys.columns
    WHERE Name = N'Status' AND Object_ID = Object_ID(N'dbo.Applications')
)
BEGIN
    ALTER TABLE dbo.Applications ADD Status nvarchar(max) NULL;
END
";
                    ctx.Database.ExecuteSqlRaw(sql);
                }
                catch
                {
                    // ignore schema patch errors
                }

                if (!ctx.Users.Any(u => u.Role == UserRole.Employee))
                {
                    var admin = new User
                    {
                        Login = "admin",
                        PasswordHash = "admin", // plaintext for educational project
                        Role = UserRole.Employee
                    };
                    ctx.Users.Add(admin);
                    ctx.SaveChanges();
                }
            }
            catch
            {
                // ignore errors during startup seeding
            }
        }
    }

}
