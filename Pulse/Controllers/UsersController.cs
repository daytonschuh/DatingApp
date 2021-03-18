using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using ModelLayer;
using BusinessLogicLayer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DTOs;

namespace Controllers
{
    [Authorize]
    public class UsersController: BaseApiController
    {
        private readonly BusinessLogicClass _businessLogicClass;
        public UsersController(BusinessLogicClass businessLogicClass)
        {
            _businessLogicClass = businessLogicClass;
        }

        // Get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsersAsync()
        {
            return Ok(await _businessLogicClass.GetUsersAsync());
        }

        // Get a single user
        [HttpGet("{username}")]
        public async Task<MemberDto> GetUserByUsernameAsync(string username)
        {
            return await _businessLogicClass.GetUserByUsernameAsync(username);
        }
    }
}