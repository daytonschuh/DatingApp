using System.Collections.Generic;
using ModelLayer;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace RepositoryLayer
{
    public class Repository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DbSet<AppUser> users;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            users = _applicationDbContext.Users;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            // eager loading with .include
            // since photos returns an appuser and appuser returns photos, we create a circular exception
            return await users.Include(p=> p.Photos).ToListAsync();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await users.Include(p=> p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<AppUser> Register(AppUser user)
        {
            await users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
            return await users.Where(x=> x == user).SingleOrDefaultAsync();
        }
    }
}