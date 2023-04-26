namespace DataLayer.Entities
{
    public class User : BaseEntity
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
