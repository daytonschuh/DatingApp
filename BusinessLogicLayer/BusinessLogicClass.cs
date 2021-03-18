using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer;

namespace BusinessLogicLayer
{
    public class BusinessLogicClass
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public BusinessLogicClass(Repository repository, IMapper mapper, ITokenService tokenService)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        // Get all users
        public async Task<IEnumerable<MemberDto>> GetUsersAsync()
        {
            var users = await _repository.GetUsersAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(users);
        }

        // Get a single user
        public async Task<MemberDto> GetUserByUsernameAsync(string username)
        {
            var user = await _repository.GetUserByUsernameAsync(username);
            return _mapper.Map<MemberDto>(user);
        }

        // Register a new user
        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            // using disposes of via IDisposable interface
            using var hmac = new HMACSHA512();

            // Create new AppUser with hashed and salted password
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
            };

            // Register the newly created user
            user = await _repository.Register(user);

            // Map returned user to UserDto
            var registeredUser = _mapper.Map<UserDto>(user);
            registeredUser.Token = _tokenService.CreateToken(user);

            // Return the newly created user
            return registeredUser;
        }

        public async Task<AppUser> CheckUsername(string username)
        {
            return await _repository.users.SingleOrDefaultAsync(x=>x.UserName == username);
        }

        // This method is not async -- Hence why we return Task.FromResult
        public Task<UserDto> CheckPassword(AppUser user, string password)
        {
            // Convert AppUser to UserDto
            UserDto udto = _mapper.Map<UserDto>(user);

            // Instantiate an hmac security
            using var hmac = new HMACSHA512(user.PasswordSalt);

            // Computer the hashed password based on salt key
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            // Check for any inconsistencies in the passwords
            for(int i = 0; i < computedHash.Length; i++){
                // If we find a problem, return null
                if(computedHash[i] != user.PasswordHash[i]) return Task.FromResult(udto);
            }

            udto.Token = _tokenService.CreateToken(user);

            // Return UserDto
            return Task.FromResult(udto);
        }

        // Check if a user with matching username already exists
        public async Task<bool> UserExists(string username)
        {
            return await _repository.users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}