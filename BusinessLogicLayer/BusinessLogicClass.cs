using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;
using RepositoryLayer;

namespace BusinessLogicLayer
{
    public class BusinessLogicClass
    {
        private readonly Repository _repository;

        public BusinessLogicClass(Repository repository)
        {
            _repository = repository;
        }

        // Get all users
        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _repository.GetUsers();
        }

        // Get a single user
        public async Task<AppUser> GetUser(int id)
        {
            return await _repository.GetUser(id);
        }
    }
}