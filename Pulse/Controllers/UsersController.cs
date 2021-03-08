using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using ModelLayer;
using BusinessLogicLayer;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly BusinessLogicClass _businessLogicClass;
        public UsersController(BusinessLogicClass businessLogicClass)
        {
            _businessLogicClass = businessLogicClass;
        }

        // Get all users
        [HttpGet]
        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _businessLogicClass.GetUsers();
        }

        // Get a single user
        [HttpGet("{id}")]
        public async Task<AppUser> GetUser(int id)
        {
            return await _businessLogicClass.GetUser(id);
        }
    }
}