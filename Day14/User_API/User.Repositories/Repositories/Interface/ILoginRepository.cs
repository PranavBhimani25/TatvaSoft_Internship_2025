﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.Entities;
using User.Entities.ViewModels;

namespace User.Repositories.Repositories.Interface
{
    public interface ILoginRepository
    {
        LoginUserResponseModel LoginUser(LoginUserRequestModel model);
        Task<string> Register(RegisterUserModel model);

        UserResponseModel LoginUserDetailById(int id);
        Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel);

        Task<UserDetail?> GetUserDetailByIdAsync(int id);

    }
}
