using System;
using System.Collections.Generic;
using System.Security.Claims;
using ECommerce.Core.Utilities.Security.Encryption;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using ECommerce.Core.Extensions;
using ECommerce.Shared.DataTransferObjects.Security;

namespace ECommerce.Core.Utilities.Security.Jwt
{
    public class JwtHelper:ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private readonly TokenOptions _tokenOptions;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessTokenDTO TokenOlustur(UserDTO user, string[] rights)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, accessTokenExpiration, user, signingCredentials, rights);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessTokenDTO()
            {
                Token = token,
                Expiration = accessTokenExpiration,
                ExpirationTime = accessTokenExpiration.Ticks
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,DateTime accessTokenExpiration, UserDTO user, SigningCredentials signingCredentials, string[] rights)
        {
            var jwt = new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audince,
                expires: accessTokenExpiration,
                notBefore:DateTime.Now,
                claims: SetClaims(user, rights),
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(UserDTO user, string[] rights)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.UserName);
            claims.AddName($"{user.Name} {user.Surname}");
            claims.AddSpecial("UserId", user.Id.ToString()); 
            claims.AddSpecial("UserGuid", user.Guid.ToString()); 
            claims.AddSpecial("UserName", user.UserName); 
         

            claims.AddRoles(rights);
            return claims;
        }
        
    }
}
 