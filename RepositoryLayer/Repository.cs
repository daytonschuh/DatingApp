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

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await users.ToListAsync();
        }

        public async Task<AppUser> GetUser(int id)
        {
            return await users.FindAsync(id);
        }

        public async Task<AppUser> Register(AppUser user)
        {
            await users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
            return await users.Where(x=> x == user).SingleOrDefaultAsync();
        }
    }
}