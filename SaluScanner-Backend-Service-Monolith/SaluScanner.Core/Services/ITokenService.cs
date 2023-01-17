using SaluScanner.Core.Configuration;
using SaluScanner.Core.DTOs;
using SaluScanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.AuthServer.Core.Service
{
    public interface ITokenService
    {
        // This is an "internal use" class, that is creating token for external IAuthenticationService to distribute to other API's.
        TokenDto CreateTokenForUser(User user);
        NonUserTokenDto CreateTokenForNonUser(Client client);
    }
}
