using System.Diagnostics.CodeAnalysis;

namespace DataLayer.Entities
{
    public class User : BaseEntity
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        [AllowNull]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
