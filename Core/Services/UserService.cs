using Core.Dtos;
using DataLayer;
using DataLayer.Entities;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        private readonly AuthorizationService authorizationService;

        public UserService(UnitOfWork unitOfWork, AuthorizationService authorizationService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public void Register(RegisterDto registerData)
        {
            if (registerData == null)
            {
                return;
            }
            string hashedPassword = authorizationService.HashPassword(registerData.Password);

            User user = new User
            {
                Email = registerData.Email,
                PasswordHash = registerData.Password,
                StudentId = registerData.StudentId,
                RoleId = registerData.RoleId
            };

            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();
        }

        public string Validate(LoginDto payload)
        {
            User user = unitOfWork.Users.GetByEmail(payload.Email);

            bool passwordFine = authorizationService.VerifyHashedPassword(user.PasswordHash, payload.Password);

            if (passwordFine)
            {
                string role = unitOfWork.Roles.GetById(user.RoleId).AssignedRole;
                return authorizationService.GetToken(user, role);
            }
            return null;
        }

        public UserAddDto AddUser(UserAddDto payload)
        {
            if (payload == null) return null;

            var isValidRole = unitOfWork.Roles.GetById(payload.RoleId);
            if (isValidRole == null) return null;

            User newUser = new User
            {
                PasswordHash = authorizationService.HashPassword(payload.Password),
                Email = payload.Email,
                RoleId = payload.RoleId,
                StudentId = payload.StudentId
            };

            unitOfWork.Users.Insert(newUser);
            unitOfWork.SaveChanges();

            return payload;
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
