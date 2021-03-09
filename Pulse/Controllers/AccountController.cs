using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ModelLayer;
using BusinessLogicLayer;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Interfaces;

namespace Controllers
{
    public class AccountController: BaseApiController
    {
        private readonly BusinessLogicClass _businessLogicClass;
        private readonly ITokenService _tokenService;

        public AccountController(BusinessLogicClass businessLogicClass, ITokenService tokenService)
        {
            _businessLogicClass = businessLogicClass;
            _tokenService = tokenService;
        }

        // Create a user
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await _businessLogicClass.UserExists(registerDto.Username)) return BadRequest("Username is taken");
            return await _businessLogicClass.Register(registerDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _businessLogicClass.CheckUsername(loginDto.Username);
            if(user == null){
                return Unauthorized("Username is invalid");
            }

            UserDto udto = await _businessLogicClass.CheckPassword(user, loginDto.Password);
            if(udto == null){
                return Unauthorized("Password is invalid");
            }

            return udto;
        }
    }
}