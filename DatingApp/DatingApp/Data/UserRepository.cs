using DatingApp.Entities;
using DatingApp.Interface;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        // The datacontext is injected to use and get the data below
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        //This method is used to get the data by id and  return the data
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        //This method is used to get the data by UserName and return the data
        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        //This methods returns AppUser data in the list.
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        //This method returns boolean value if savechanges occurs
        public async Task<bool> SaveallAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        // This method is used to update the data by user_name or the id
        public void update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
