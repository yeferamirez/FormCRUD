using Form.Domain.Entities;
using Form.Domain.Entities.Requests;

namespace Form.Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);
        public Task<User> GetUserById(int id);
        public Task<List<User>> GetUsers();
        public Task<List<User>> GetUserByDetails(UserRequest userRequest);
        public Task<int> CountUsers();
        public Task<User> UpdateUser(User actualUser, User userRequest);
        public Task<User> DeleteUser(User userRequest);
    }
}
