using Form.Domain.Entities;
using Form.Domain.Entities.Requests;
using Form.Infrastructure.Context;
using Form.Infrastructure.Interfaces.Repositories;
using Form.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Form.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        protected DbSet<User> _dbSet;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
            _dbSet = _userContext.Set<User>();
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                await _dbSet.AddAsync(user);
                await _userContext.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<User>> GetUserByDetails(UserRequest userRequest)
        {
            var query = _dbSet.AsQueryable();
            BuildObjectsUtil.BuildUserPagination(userRequest, query);
            var users = await query
                    .OrderBy(u => u.FirstName)
                    .Skip((userRequest.PageNumber - 1) * userRequest.PageSize)
                    .Take(userRequest.PageSize)
                    .ToListAsync();

            return users;
        }

        public async Task<int> CountUsers()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<User> UpdateUser(User actualUser, User userRequest)
        {
            _userContext.Entry(actualUser).CurrentValues.SetValues(userRequest);
            if (_userContext.Entry(actualUser).State == EntityState.Modified)
                await _userContext.SaveChangesAsync();

            return userRequest;
        }

        public async Task<User> DeleteUser(User userRequest)
        {
            _dbSet.Remove(userRequest);
            await _userContext.SaveChangesAsync();

            return userRequest;
        }
    }
}
