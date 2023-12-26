using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Workers.Server.Model.DTOs;
using Workers.Server.Model.Interfaces;
using Workers.Server.Models;

namespace Workers.Server.Model.Services
{
    public class IdentityUserService : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly JWTTokenService _jwtToken;

        public IdentityUserService(UserManager<ApplicationUser> userManager, JWTTokenService jwtToken, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtToken = jwtToken;
            _signInManager = signInManager;
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
                IList<string> roles = new List<string>() { "User Admin"};
                await _userManager.AddToRolesAsync(user, roles);
                return new UserDTO
                {
                    ID = user.Id,
                    UserName = user.UserName,
                    Token = await _jwtToken.GetToken(user, System.TimeSpan.FromMinutes(100)),
                    Location = user.Location,
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

        public async Task<UserDTO> RegisterAsAWorker(RegisterDTO register, ModelStateDictionary modelState, ClaimsPrincipal principal)
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
                IList<string> roles = new List<string>() { "Worker Admin" };
                await _userManager.AddToRolesAsync(user, roles);
                return new UserDTO
                {
                    ID = user.Id,
                    UserName = user.UserName,
                    Location = user.Location,
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

        public async Task<UserDTO> RegisterAsManager(RegisterDTO register, ModelStateDictionary modelState, ClaimsPrincipal principal)
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
                IList<string> roles = new List<string>() { "Admin Manager" };
                await _userManager.AddToRolesAsync(user, roles);
                return new UserDTO
                {
                    ID = user.Id,
                    UserName = user.UserName,
                    Token = await _jwtToken.GetToken(user, System.TimeSpan.FromMinutes(100)),
                    Location = user.Location,
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

        public async Task Logout(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            if (user != null)
            {
                var jtiClaim = principal.FindFirst(JwtRegisteredClaimNames.Jti);
                if (jtiClaim != null)
                {
                    var jti = jtiClaim.Value;
                    _jwtToken.RevokeToken(jti);
                }
            }

            await _signInManager.SignOutAsync();
        }
    }
}
