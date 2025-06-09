using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Entities.Context;
using User.Entities.Entities;
using User.Repositories.Repositories.Interface;

namespace User.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDetails>> GetAllAsync()
        {
            //var userlist = _context.Users.ToListAsync();

            return await _context.Users.Where(u => !u.IsDeleted ).ToListAsync();
        }

        public async Task<UserDetails> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<UserDetails> AddAsync(UserDetails user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(UserDetails user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            if(user.IsDeleted ==  false) { 
                user.IsDeleted = true;

                _context.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
