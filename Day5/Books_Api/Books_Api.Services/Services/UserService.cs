using Books_Api.Entities.Entities;
using Books_Api.Entities.Repositories.Interface;
using Books_Api.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Api.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // To Add User
        public async Task AddUser(User user)
        {
            await this._userRepository.AddUser(user);
        }

        // Login API
        public User? Login(string email, string password)
        {
            return this._userRepository.Login(email, password);
        }
    }
}
