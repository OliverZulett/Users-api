using Microsoft.AspNetCore.Mvc;
using Users.Models.Data;
using Users.Models.Repository;
using Users.Services;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IUserService _userService;

        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        [ActionName(nameof(GetUserAsync))]
        public IEnumerable<User> GetUserAsync() 
        {
            return _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetUserById))]
        public ActionResult<User> GetUserById(Guid id)
        {
            var userById = _userService.GetUserById(id);
            if (userById is null)
            {
                return NotFound();
            }
            return userById;
        }

        [HttpPost]
        [ActionName(nameof(CreateUserAsync))]
        public async Task<ActionResult<User>> CreateUserAsync(User user)
        {
            await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdateUser))]
        public async Task<ActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.Id)
            {
               return BadRequest();
            }
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user is null)
            {
                return NotFound(nameof(user));
            }
            await _userRepository.DeleteUserAsync(user);
            return NoContent();
        }
    }
}
