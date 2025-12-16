using Crud_Api.Dtos;
using Crud_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        public UsersController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Users>> AddUser([FromBody] UserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new Users
            {
                UserName = dto.username,
                password = BCrypt.Net.BCrypt.HashPassword(dto.password) //dto.password  Install-Package BCrypt.Net-Next

            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User added successfully");
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>>GetOneUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult>Edit(int id, [FromBody] UserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            user.UserName = dto.username;
            user.password = dto.password;
            await _context.SaveChangesAsync();
            return Ok(new { message = "User Updated!" });

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>Delete(int id)
        {
            var deleteusers = await _context.Users.Where(u => u.IdUser == id).ExecuteDeleteAsync();
            if(deleteusers > 0)
            {
                await _context.Users.ToListAsync();
            }
            return Ok("User deleted!");
        }
    }
}
