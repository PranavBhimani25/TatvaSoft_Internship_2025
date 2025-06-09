using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.Context;
using User.Entities.Entities;
using User.Entities.ViewModels;
using User.Repositories.Repositories.Interface;

namespace User.Repositories.Repositories
{
    public class LoginRepository(UserDbContext cIDbContext) : ILoginRepository
    {
        private readonly UserDbContext _cIDbContext = cIDbContext;

        public LoginUserResponseModel LoginUser(LoginUserRequestModel model)
        {
            var existingUser = _cIDbContext.Users
                .FirstOrDefault(u => u.Email.ToLower() == model.EmailAddress.ToLower() && !u.IsDeleted);

            if (existingUser == null)
            {
                return new LoginUserResponseModel() { Message = "Email Address Not Found." };
            }
            if (existingUser.Password != model.Password)
            {
                return new LoginUserResponseModel() { Message = "Incorrect Password." };
            }

            return new LoginUserResponseModel
            {
                Id = existingUser.Id,
                Name = existingUser.Name ?? string.Empty,
                PhoneNumber = existingUser.PhoneNumber,
                Email = existingUser.Email,
                Role = existingUser.Role,
                Message = "Login Successfully"
            };
        }

        public async Task<string> Register(RegisterUserModel model)
        {
            var isExist = _cIDbContext.Users.Where(x => x.Email == model.EmailAddress && !x.IsDeleted).FirstOrDefault();

            if (isExist != null) throw new Exception("Email already exist");

            UserDetails user = new UserDetails()
            {
                Name = model.Name,
                Email = model.EmailAddress,
                Password = model.Password,
                Role = "user",
                PhoneNumber = model.PhoneNumber,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            await _cIDbContext.Users.AddAsync(user);
            _cIDbContext.SaveChanges();
            return "User Added!";
        }
    }
}
