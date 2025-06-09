using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.Entities;
using User.Entities.ViewModels;
using User.Services.Services.Interface;
using User.Repositories.Repositories.Interface;
using User.Repositories.Helper;

namespace User.Services.Services
{
    public class LoginService(ILoginRepository loginRepository, JwtService jwtService) : ILoginService
    {
        private readonly ILoginRepository _loginRepository = loginRepository;
        private readonly JwtService _jwtService = jwtService;
        ResponseResult result = new ResponseResult();

        public ResponseResult LoginUser(LoginUserRequestModel model)
        {
            var userObj = UserLogin(model);

            if (userObj.Message.ToString() == "Login Successfully")
            {
                result.Message = userObj.Message;
                result.Result = ResponseStatus.Success;
                result.Data = _jwtService.GenerateToken(userObj.Id.ToString(), userObj.Name, userObj.PhoneNumber, userObj.Email, userObj.Role);
            }
            else
            {
                result.Message = userObj.Message;
                result.Result = ResponseStatus.Error;
            }
            return result;
        }

        public LoginUserResponseModel UserLogin(LoginUserRequestModel model)
        {
            return _loginRepository.LoginUser(model);
        }

        public Task<string> Register(RegisterUserModel model)
        {
            return _loginRepository.Register(model);
        }
    }
}
