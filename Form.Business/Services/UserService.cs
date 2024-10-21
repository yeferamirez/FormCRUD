using Form.Domain.Constants;
using Form.Domain.Entities;
using Form.Domain.Entities.Requests;
using Form.Domain.Entities.Responses;
using Form.Infrastructure.Interfaces.Repositories;
using Form.Infrastructure.Utils;
using Newtonsoft.Json;
using Serilog;

namespace Form.Business.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public virtual async Task<UserResponse> CreateUser(User userRequest)
        {
            try
            {
                var user = BuildObjectsUtil.BuildUser(userRequest);
                var userResponse = await _userRepository.CreateUser(user);

                return new UserResponse(ResponseConstants.SUCCESS_PROCESS, userResponse);
            }
            catch (Exception)
            {
                Log.Error($"There was an error when trying to create user - {JsonConvert.SerializeObject(userRequest)}");
                throw;
            }
        }

        public virtual async Task<UserResponse> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);

                if (user == null)
                    return new UserResponse(ResponseConstants.FAILED_PROCESS, ExceptionConstants.USER_NOT_EXIST);

                return new UserResponse(ResponseConstants.SUCCESS_PROCESS, user);
            }
            catch (Exception)
            {
                Log.Error($"There was an error when trying to get user by id - {id}");
                throw;
            }
        }

        public virtual async Task<UserResponse> GetUsers()
        {
            try
            {
                var userResponse = await _userRepository.GetUsers();

                if (userResponse == null || userResponse.Count == 0)
                    return new UserResponse(ResponseConstants.SUCCESS_PROCESS, ExceptionConstants.USERS_NULL);

                return new UserResponse(ResponseConstants.SUCCESS_PROCESS, userResponse);
            }
            catch (Exception)
            {
                Log.Error("There was an error when trying to get users");
                throw;
            }
        }

        public virtual async Task<UserResponse> GetUserByDetails(UserRequest userRequest)
        {
            try
            {
                var numberUsers = await _userRepository.CountUsers();
                var users = new List<User>();
                if (numberUsers > 0)
                {
                    var totalPages = (int)Math.Ceiling((double)numberUsers / userRequest.PageSize);
                    users = await _userRepository.GetUserByDetails(userRequest);
                    return new UserResponse(ResponseConstants.SUCCESS_PROCESS, users);
                }

                return new UserResponse(ResponseConstants.SUCCESS_PROCESS, ExceptionConstants.USERS_BY_CONDITION_NULL);
            }
            catch (Exception)
            {
                Log.Error($"There was an error when trying to get users by details - {JsonConvert.SerializeObject(userRequest)}");
                throw;
            }
        }

        public virtual async Task<UserResponse> UpdateUser(User userRequest)
        {
            try
            {
                var userResponse = await _userRepository.GetUserById(userRequest.Id);

                if (userResponse == null)
                    return new UserResponse(ResponseConstants.FAILED_PROCESS, ExceptionConstants.USER_NOT_EXIST);

                userRequest.CreationDate = userResponse.CreationDate;
                userRequest.ModificationDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                var userUpdated = await _userRepository.UpdateUser(userResponse, userRequest);
                return new UserResponse(ResponseConstants.SUCCESS_PROCESS, userUpdated);
            }
            catch (Exception)
            {
                Log.Error($"There was an error when trying to update user - {JsonConvert.SerializeObject(userRequest)}");
                throw;
            }
        }

        public virtual async Task<UserResponse> DeleteUser(int id)
        {
            try
            {
                var userResponse = await _userRepository.GetUserById(id);

                if (userResponse == null)
                    return new UserResponse(ResponseConstants.FAILED_PROCESS, ExceptionConstants.USER_NOT_EXIST);

                var user = await _userRepository.DeleteUser(userResponse);

                return new UserResponse(ResponseConstants.SUCCESS_PROCESS, user);
            }
            catch (Exception)
            {
                Log.Error($"There was an error when trying to delete user con id - {id}");
                throw;
            }
        }
    }
}
