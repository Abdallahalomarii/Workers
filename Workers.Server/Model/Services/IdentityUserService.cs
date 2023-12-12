using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Interfaces;
using Workers.Server.Models;

namespace Workers.Server.Model.Services
{
    public class IdentityUserService : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly JWTTokenService _jwtToken;

        public IdentityUserService(UserManager<ApplicationUser> userManager, JWTTokenService jwtToken)
        {
            _userManager = userManager;
            _jwtToken = jwtToken;
        }

        public async Task<UserDTO> Register(RegisterDTO register, ModelStateDictionary modelState, ClaimsPrincipal principal)
        {
            var user = new ApplicationUser()
            {
                UserName = register.UserName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                Location = register.Location
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, register.Roles);
                return new UserDTO
                {
                    ID = user.Id,
                    UserName = user.UserName,
                    Token = await _jwtToken.GetToken(user, System.TimeSpan.FromMinutes(100)),
                    Roles = await _userManager.GetRolesAsync(user)

                };
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    var errorMessage = error.Code.Contains("Password") ? nameof(register.Password) :
                                       error.Code.Contains("Email") ? nameof(register.Email) :
                                       error.Code.Contains("Username") ? nameof(register.UserName) :
                                         error.Code.Contains("PhoneNumber") ? nameof(register.PhoneNumber) :
                                         error.Code.Contains("Location") ? nameof(register.Location) :
                                       "";
                    modelState.AddModelError(errorMessage, error.Description);

                };
                return null;
            }
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
           var user = await _userManager.FindByNameAsync(username);
            bool isValidPass = await _userManager.CheckPasswordAsync(user, password);
            if (isValidPass)
            {
                return new UserDTO
                {
                    ID = user.Id,
                    UserName = user.UserName,
                    Location = user.Location,
                    Token = await _jwtToken.GetToken(user, System.TimeSpan.FromMinutes(100)),
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }
            return null;
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal); 
            if (user == null)
            {
                return null;
            }
            return new UserDTO
            {
                ID = user.Id,
                UserName = user.UserName,
                Location = user.Location,
                Token = await _jwtToken.GetToken(user, System.TimeSpan.FromMinutes(100)),
                Roles = await _userManager.GetRolesAsync(user)
            };
        }
        

        
    }
}
