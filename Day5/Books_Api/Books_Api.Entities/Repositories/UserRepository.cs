using Books_Api.Entities.Context;
using Books_Api.Entities.Entities;
using Books_Api.Entities.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Api.Entities.Repositories
{
    public class UserRepository(BookDbContext bookDbContext) : IUserRepository
    {
        private readonly BookDbContext _dbContext = bookDbContext;

        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public User? Login(string username, string password)
        {
            var user = _dbContext.Users.Where(x => x.Email == username && x.Password == password).FirstOrDefault();
            return user;
        }
    }
}
