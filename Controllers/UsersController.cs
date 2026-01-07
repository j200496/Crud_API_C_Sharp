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

        [HttpGet("ActiveUser")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetActiveUsers()
        {
            var activeusers = await _context.Users.Where(u => u.IsActive)
                .Select(u => new UserDto
                {
                    username = u.UserName,                 
                    IsActive = u.IsActive,
                }).ToListAsync();
            return Ok(activeusers);

        }
        [HttpPost]
        public async Task<ActionResult<Users>> AddUser([FromBody] UserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new Users
            {
                UserName = dto.username,
                password = BCrypt.Net.BCrypt.HashPassword(dto.password), //dto.password  Install-Package BCrypt.Net-Next
                
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
        [HttpGet("Search")]
        public async Task<ActionResult>SearchUser(string username)
        {
            var finduser = await _context.Users.Where(u => u.UserName.Contains(username)).ToListAsync();
           
            if(finduser == null || finduser.Count == 0)
            {
                return Ok(_context.Users.ToListAsync());
            }

            if (string.IsNullOrEmpty(username))
            {

                return Ok(_context.Users.ToListAsync());
            }
            return Ok(finduser);
        }
        [HttpGet("CountUsers")]
        public async Task<ActionResult<int>> CountUsers()
        {
            var count = await _context.Users.CountAsync();
            if (count == 0 || count < 0)
            {
                return NotFound(new {message = "There are no users to count"});
            }
            return Ok(count);
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
            user.IsActive = dto.IsActive;
            await _context.SaveChangesAsync();
            return Ok(new { message = "User Updated!" });

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>Delete(int id)
        {

            /* var deleteusers = await _context.Users.Where(u => u.IdUser == id).ExecuteDeleteAsync();
             if(deleteusers > 0)
             {
                 await _context.Users.ToListAsync();
             }
             return Ok("User deleted!");
            Add-Migration AddIsActiveToUser
            Update-Database

            */
            var finduser = await _context.Users.FindAsync(id);
            if(finduser == null)
            {
                return NotFound();
            }
            finduser.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok(new { message = "User Deleted" });
        }
    }
}
