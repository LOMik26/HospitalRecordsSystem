using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Domain.Enums;
namespace Hospital.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        // Роль: Patient или Employee (enum вместо строки)
        public UserRole Role { get; set; }

        // При необходимости связываем с сущностью Patient
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
