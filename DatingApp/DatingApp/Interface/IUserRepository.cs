using DatingApp.Entities;

namespace DatingApp.Interface
{
    public interface IUserRepository
    {
        void update(AppUser user);

        Task<bool> SaveallAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByNameAsync(string Username);

    }
}
