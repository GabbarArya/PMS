using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pms_api.Context;
using pms_api.Models;

namespace pms_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext authContext)
        {
            _authContext = authContext; 
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if(userObj == null)
                return BadRequest();
            var user = await _authContext.Users.FirstOrDefaultAsync(x => x.username == userObj.username && x.password ==userObj.password);
            if (user == null)
                return NotFound(new { Messange = "User Not Found!" });
            return Ok(new
            {
                Message = "login Success!"
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();
            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
            return Ok(new{
                Message = "User Reistered!"
            });
        }
    }
}
