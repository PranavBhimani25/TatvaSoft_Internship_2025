using Microsoft.EntityFrameworkCore;
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

        public UserResponseModel LoginUserDetailById(int id)
        {
            var userDetail = _cIDbContext.Users
                .Where(u => u.Id == id && !u.IsDeleted)
                .Select(user => new UserResponseModel()
                {
                    Id = user.Id,
                    FirstName = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    EmailAddress = user.Email,
                    UserType = user.Role,
                })
                .FirstOrDefault() ?? throw new Exception("User not found.");

            return userDetail;
        }

        public async Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel)
        {
            try
            {
                var user = _cIDbContext.Users.Where(x => x.Id == requestModel.UserId).FirstOrDefault();

                if (user == null) throw new Exception("Not Found!");

                var userDetails = _cIDbContext.UserDetails.Where(x => x.UserId == requestModel.UserId).FirstOrDefault();

                if (userDetails == null)
                {
                    // Add User Details
                    UserDetail userDetail = new UserDetail()
                    {
                        UserId = requestModel.UserId,
                        Availability = requestModel.Availability,
                        CityId = requestModel.CityId,
                        CountryId = requestModel.CountryId,
                        Department = requestModel.Department,
                        EmployeeId = requestModel.EmployeeId,
                        LinkedInUrl = requestModel.LinkedInUrl,
                        Manager = requestModel.Manager,
                        MyProfile = requestModel.MyProfile,
                        MySkills = requestModel.MySkills,
                        Surname = requestModel.Surname,
                        Name = requestModel.Name,
                        UserImage = requestModel.UserImage,
                        WhyIVolunteer = requestModel.WhyIVolunteer,
                        Status = requestModel.Status,
                        Title = requestModel.Title,

                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                    };

                    await _cIDbContext.UserDetails.AddAsync(userDetail);
                }
                else
                {
                    // Update User Details
                    userDetails.UserId = requestModel.UserId;
                    userDetails.Availability = requestModel.Availability;
                    userDetails.CityId = requestModel.CityId;
                    userDetails.CountryId = requestModel.CountryId;
                    userDetails.Department = requestModel.Department;
                    userDetails.EmployeeId = requestModel.EmployeeId;
                    userDetails.LinkedInUrl = requestModel.LinkedInUrl;
                    userDetails.Manager = requestModel.Manager;
                    userDetails.MyProfile = requestModel.MyProfile;
                    userDetails.MySkills = requestModel.MySkills;
                    userDetails.Surname = requestModel.Surname;
                    userDetails.Name = requestModel.Name;
                    userDetails.UserImage = requestModel.UserImage;
                    userDetails.WhyIVolunteer = requestModel.WhyIVolunteer;
                    userDetails.Status = requestModel.Status;
                    userDetails.Title = requestModel.Title;

                    userDetails.ModifiedDate = DateTime.Now;

                    _cIDbContext.UserDetails.Update(userDetails);
                }

                user.Name = requestModel.Name;

                _cIDbContext.Users.Update(user);
                await _cIDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserDetail?> GetUserDetailByIdAsync(int id)
        {
            return await _cIDbContext.Set<UserDetail>()
                .Include(u => u.User) // if you want to include related UserDetails entity
                .FirstOrDefaultAsync(u => u.UserId == id);
        }
    }
}
