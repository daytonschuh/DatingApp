using System.Collections.Generic;
using ModelLayer;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper.QueryableExtensions;
using DTOs;
using AutoMapper;

namespace RepositoryLayer
{
    public class Repository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DbSet<AppUser> users;

        public Repository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            users = _applicationDbContext.Users;
        }

        public async Task<IEnumerable<MemberDto>> GetUsersAsync()
        {
            // eager loading with .include
            // since photos returns an appuser and appuser returns photos, we create a circular exception
            return await users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<MemberDto> GetUserByUsernameAsync(string username)
        {
            return await users.Where(x => x.UserName == username).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<AppUser> Register(AppUser user)
        {
            await users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
            return await users.Where(x=> x == user).SingleOrDefaultAsync();
        }
    }
}