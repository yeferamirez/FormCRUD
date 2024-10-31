using Form.Business.Services;
using Form.Business.Utils;
using Form.Domain.Entities;
using Form.Domain.Entities.Requests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace Form.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                var userValidator = new UserValidations();
                var resultValidation = userValidator.Validate(user);

                if (!resultValidation.IsValid)
                {
                    var errores = resultValidation.Errors.Select(v => v.ErrorMessage).ToList();

                    return BadRequest(new { Errores = errores.Select(e => e) });
                }
                var response = await _userService.CreateUser(user);

                if (response.User == null)
                    return BadRequest(response);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception e)
            {
                Log.Error(JsonConvert.SerializeObject(e));
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var response = await _userService.GetUsers();

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception e)
            {
                Log.Error(JsonConvert.SerializeObject(e));
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("get-user-by-id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var response = await _userService.GetUserById(id);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception e)
            {
                Log.Error(JsonConvert.SerializeObject(e));
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("get-users-by-details")]
        public async Task<IActionResult> GetUsersById([FromQuery] UserRequest user)
        {
            try
            {
                var users = await _userService.GetUserByDetails(user);

                return StatusCode(StatusCodes.Status200OK, users);
            }
            catch (Exception e)
            {
                Log.Error(JsonConvert.SerializeObject(e));
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                var userValidator = new UserValidations();
                var resultValidation = userValidator.Validate(user);
                if (!resultValidation.IsValid)
                {
                    var errores = resultValidation.Errors.Select(v => v.ErrorMessage).ToList();

                    return BadRequest(new { Errores = errores.Select(e => e) });
                }
                var response = await _userService.UpdateUser(user);
                if (response == null)
                    return StatusCode(StatusCodes.Status400BadRequest);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception e)
            {
                Log.Error(JsonConvert.SerializeObject(e));
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var response = await _userService.DeleteUser(id);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception e)
            {
                Log.Error(JsonConvert.SerializeObject(e));
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
