using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Users.Models.Data;
using Users.Models.Repository;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [ActionName(nameof(GetUserAsync))]
        public IEnumerable<User> GetUserAsync() 
        {
            return _userRepository.GetAllUsers();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetUserById))]
        public ActionResult<User> GetUserById(int id)
        {
            var userById = _userRepository.GetUserById(id);
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
            await _userRepository.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdateUser))]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
               return BadRequest();
            }
            await _userRepository.updateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user is null)
            {
                return NotFound(nameof(user));
            }
            await _userRepository.DeleteUserAsync(user);
            return NoContent();
        }
    }
}
