using Core.Dtos;
using DataLayer;
using DataLayer.Entities;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        private AuthorizationService authorizationService;

        public UserService(UnitOfWork unitOfWork, AuthorizationService authorizationService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public RegisterDto Register(RegisterDto registerData)
        {
            if (registerData == null)
            {
                return null;
            }

            var isValidRole = unitOfWork.Roles.GetById(registerData.RoleId);
            if (isValidRole == null) return null;

            string hashedPassword = authorizationService.HashPassword(registerData.Password);

            User user = new User
            {
                Email = registerData.Email,
                PasswordHash = hashedPassword,
                StudentId = registerData.StudentId,
                AvailableRoleId = registerData.RoleId
            };

            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();

            return registerData;
        }

        public string Validate(LoginDto payload)
        {
            User user = unitOfWork.Users.GetByEmail(payload.Email);
            if (user == null)
                return null;

            bool passwordFine = authorizationService.VerifyHashedPassword(user.PasswordHash, payload.Password);

            if (passwordFine)
            {
                string role = unitOfWork.Roles.GetById(user.AvailableRoleId).AssignedRole;
                return authorizationService.GetToken(user, role);
            }
            return null;
        }

        public bool ValidateToken(string tokenString)
        {
            return authorizationService.ValidateToken(tokenString);
        }

        public List<User> GetAll()
        {
            List<User> results = unitOfWork.Users.GetAll();

            return results;
        }

        public User GetById(int userId)
        {
            User user = unitOfWork.Users.GetById(userId);

            return user;
        }
    }
}
