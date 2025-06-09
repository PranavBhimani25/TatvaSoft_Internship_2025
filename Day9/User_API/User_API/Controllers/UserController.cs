using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Entities.Entities;
using User.Entities.ViewModels;
using User.Repositories.Repositories.Interface;

namespace User_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetUserList")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserListById")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> Create(UserDetails user)
        {
            var newUser = await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updatedUser)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.LastUpdatedDate = updatedUser.LastUpdatedDate;

            await _userRepository.UpdateAsync(user);

            return Ok(user);
        }


        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userRepository.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }

}
