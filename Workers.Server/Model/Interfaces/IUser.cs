using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Workers.Server.Model.DTOs;

namespace Workers.Server.Model.Interfaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterDTO register, ModelStateDictionary modelState, ClaimsPrincipal principal);

        public Task<UserDTO> Authenticate(string username, string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal principal);

    }
}
