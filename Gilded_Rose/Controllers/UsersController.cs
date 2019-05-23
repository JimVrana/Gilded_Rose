using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gilded_Rose.Core.Models;
using Gilded_Rose.Core.Interfaces;

namespace Gilded_Rose.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]ApiUser userParam)
        {
            var user = _userService.Authenticate(userParam.UserName, userParam.Password);

            if(user == null)
            {
                return BadRequest(new { message = "UserName or Password is incorrect" });
            }

            return Ok(user);
        }
    }
}
