using System.Diagnostics.CodeAnalysis;

namespace DataLayer.Entities
{
    public class User : BaseEntity
    {
        public int AvailableRoleId { get; set; }

        public AvailableRole AvailableRole { get; set; }

        [AllowNull]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
