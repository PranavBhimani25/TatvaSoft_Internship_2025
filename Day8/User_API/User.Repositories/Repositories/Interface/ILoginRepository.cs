using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.ViewModels;

namespace User.Repositories.Repositories.Interface
{
    public interface ILoginRepository
    {
        LoginUserResponseModel LoginUser(LoginUserRequestModel model);

        Task<string> Register(RegisterUserModel model);
    }
}
