using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaluScanner.AuthServer.Core.Service;
using SaluScanner.Core.Configuration;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using SaluScanner.Core.Repositories;
using SaluScanner.Core.Service;
using SaluScanner.Core.UnitOfWorks;
using SaluScanner.SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;

        public AuthenticationService(IOptions<List<Client>> optionsClients, ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenRepository)
        {
            _clients = optionsClients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            if(user == null)
            {
                return Response<TokenDto>.Fail("Email or Password Wrong!",404,true);
            }

            if (!(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
            {
                return Response<TokenDto>.Fail("Email or Password Wrong!", 404,true);
            }

            var token = _tokenService.CreateTokenForUser(user);

            var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenRepository.AddAsync(new UserRefreshToken
                {
                    UserId = user.Id,
                    Token = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                });
            }
            else
            {
                userRefreshToken.Token = token.RefreshToken;
                userRefreshToken.Expiration= token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(token,200);
        }

        public Response<NonUserTokenDto> CreateTokenByNonUser(NonUserLoginDto nonUserLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == nonUserLoginDto.ClientId && x.Secret == nonUserLoginDto.ClientSecret);

            if(client == null)
            {
                return Response<NonUserTokenDto>.Fail("No such client found", 404,true);
            }

            var token = _tokenService.CreateTokenForNonUser(client);

            return Response<NonUserTokenDto>.Success(token,200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Token == refreshToken).SingleOrDefaultAsync();
        
            if(existRefreshToken == null)
            {
                return Response<TokenDto>.Fail("Refresh token not found.", 404, true);
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            // not necessary but defensive
            if(user == null)
            {
                return Response<TokenDto>.Fail("Refresh token or user not found.", 404, true);
            }

            var tokenDto = _tokenService.CreateTokenForUser(user);

            existRefreshToken.Token = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.AccessTokenExpiration;

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(tokenDto,200);
        }

        public async Task<Response<NoDataDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Token == refreshToken).SingleOrDefaultAsync();

            if(existRefreshToken == null)
            {
                return Response<NoDataDto>.Fail("Refresh token not found.", 404, true);
            }

            _userRefreshTokenRepository.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(200);
        }
    }
}
