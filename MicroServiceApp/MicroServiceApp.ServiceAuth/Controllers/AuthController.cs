using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MicroServiceApp.InfrastructureLayer.Auth;
using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.InfrastructureLayer.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using MicroServiceApp.ServiceAuth.Services;

namespace MicroServiceApp.ServiceAuth.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAsyncHttpClientUser<User> asyncHttpClientUser;
        private readonly IAsyncServiceVerifyUser<string, AuthDto, User> asyncServiceVerifyUser;

        public AuthController(IAsyncHttpClientUser<User> asyncHttpClientUser,
            IAsyncServiceVerifyUser<string, AuthDto, User> asyncServiceVerifyUser)
        {
            this.asyncServiceVerifyUser = asyncServiceVerifyUser;
            this.asyncHttpClientUser = asyncHttpClientUser;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] AuthDto authDto)
        {

            if (ModelState.IsValid)
            {
                User user = await asyncHttpClientUser.GetByEmail(authDto.Email);
                string resultVerify = asyncServiceVerifyUser.VerifyUser(authDto, user);
                if (resultVerify != null)
                {
                    return BadRequest(resultVerify);
                }
                var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RoleName)
            };
                ClaimsIdentity claimsIdentity =
                    new(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claimsIdentity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var responseJson = new
                {
                    access_token = encodedJwt,
                    email = user.Email,
                    role=user.Role.RoleName
                };

                return Json(responseJson);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
