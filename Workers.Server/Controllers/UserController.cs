using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Interfaces;

namespace Workers.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
             _user = user;
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult<UserDTO>> SignUpAsUser(RegisterDTO registerDTO)
        {
            var user = await _user.Register(registerDTO, this.ModelState, User);
            if (ModelState.IsValid)
            {
                if (user != null)
                    return user;

                else
                    return NotFound();
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("RegisterWorker")]
        public async Task<ActionResult<UserDTO>> SignUpAsWorker(RegisterDTO registerDTO)
        {
            var user = await _user.RegisterAsAWorker(registerDTO, this.ModelState, User);
            if (ModelState.IsValid)
            {
                if (user != null)
                    return user;

                else
                    return NotFound();
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }
        
        [HttpPost("RegisterManager")]
        public async Task<ActionResult<UserDTO>> SignupManager(RegisterDTO registerDTO)
        {
            var user = await _user.RegisterAsManager(registerDTO, this.ModelState, User);
            if (ModelState.IsValid)
            {
                if (user != null)
                    return user;

                else
                    return NotFound();
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Signin(LoginDTO loginDto)
        {
            var user = await _user.Authenticate(loginDto.Username, loginDto.Password);

            if (ModelState.IsValid)
            {
                if (user != null)
                    return user;

                else
                    return Unauthorized();
            }
            return BadRequest(new ValidationProblemDetails(ModelState));

        }

        [HttpGet("Profile")]
        public async Task<ActionResult<UserDTO>> Profile()
        {
            var profile = await _user.GetUser(User);
            return Ok(profile);
        }

        [HttpPost("Logout")]
        //[Authorize]
        public async Task<ActionResult> Logout()
        {
            await _user.Logout(User);
            return Ok();
        }
    }
}
