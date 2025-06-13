using User.Entities.ViewModels;
using User.Entities.Entities;

namespace User.Services.Services.Interface
{
    public interface ILoginService
    {
        ResponseResult LoginUser(LoginUserRequestModel model);

        LoginUserResponseModel UserLogin(LoginUserRequestModel model);
        Task<string> Register(RegisterUserModel model);

        UserResponseModel LoginUserDetailById(int id);
        Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel);
        Task<UserDetail?> GetUserDetailByIdAsync(int id);

    }
}
