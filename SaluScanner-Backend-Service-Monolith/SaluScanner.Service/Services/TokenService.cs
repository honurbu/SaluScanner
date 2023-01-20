using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SaluScanner.AuthServer.Core.Service;
using SaluScanner.Core.Configuration;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using SaluScanner.SharedLibrary.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SaluScanner.Core.DTOs;
using Azure.Core;

namespace SaluScanner.Service.Services
{
    public class TokenService : ITokenService
    {
        // Built-in class of IdentityServer
        private readonly UserManager<User> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(UserManager<User> userManager, IOptions<CustomTokenOption> tokenOption)
        {
            _userManager = userManager;
            _tokenOption = tokenOption.Value;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaimsByUser(User user, List<String> audiences)
        {
            var userClaimList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            userClaimList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userClaimList;
        }
        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();

            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());

            return claims;
        }

        public TokenDto CreateTokenForUser(User userApp)
        {

            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsByUser(userApp, _tokenOption.Audience),
                signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,

            };

            return tokenDto;

        }
        public NonUserTokenDto CreateTokenForNonUser(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
           
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                // available until now
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials
            );

            // Initializing and Serializing Token
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.WriteToken(jwtSecurityToken);

            var clientTokenDto = new NonUserTokenDto
            {
                AccessToken = token,

                AccessTokenExpiration = accessTokenExpiration
            };

            return clientTokenDto;
        }
    }
}