using User.Entities.Entities;
namespace User.Repositories.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDetails>> GetAllAsync();
        Task<UserDetails> GetByIdAsync(int id);
        Task<UserDetails> AddAsync(UserDetails user);
        Task UpdateAsync(UserDetails user);
        Task<bool> DeleteAsync(int id);
    }

}
