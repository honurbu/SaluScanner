using Microsoft.AspNetCore.Identity;
using SaluScanner.AuthServer.Core.Service;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using SaluScanner.Core.UnitOfWorks;
using SaluScanner.Service.Mapping;
using SaluScanner.SharedLibrary.DTOs;


namespace SaluScanner.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName,
            };

            // Password hashing
            var result = await _userManager.CreateAsync(user,createUserDto.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Response<UserDto>.Fail(new ErrorDto(errors,true), 400);
            }

            await _unitOfWork.CommitAsync();

            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user),200);
        }

        public async Task<Response<UserDto>> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user == null)
            {
                return Response<UserDto>.Fail("No such user found!", 400,true);
            }

            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(user),200);

        }
    }
}
