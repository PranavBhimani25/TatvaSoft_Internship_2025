using Books_Api.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Api.Services.Services.Interface
{
    public interface IUserService
    {
        Task AddUser(User user);
        User? Login(string username, string password);
    }
}
