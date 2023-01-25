using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Services;
using SaluScanner.SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.AuthServer.Core.Service
{
    public interface IUserService 
    {
        // This class has no Repository Layer implementation, because of this IdentityServer Framework have implemantations for it.
        // This class is for creating new user etc.

        Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<Response<UserDto>> GetUserByUserNameAsync(string userName);
    }
}
