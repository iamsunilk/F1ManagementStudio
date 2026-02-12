using F1ManagementStudioWebAPI.Models.DTOs;
using F1ManagementStudioWebAPI.Models.DTOs.RegisterLoginDtos;
using F1ManagementStudioWebAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace F1ManagementStudioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> usermanager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> usermanager,ITokenRepository tokenRepository)
        {
            this.usermanager = usermanager;
            this.tokenRepository = tokenRepository;
        }

        //Post : Rigister


        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
            };
            var identityResult = await usermanager.CreateAsync(identityUser, registerRequest.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequest.Roles != null && registerRequest.Roles.Any())
                {
                    identityResult = await usermanager.AddToRolesAsync(identityUser, registerRequest.Roles);
                    if (identityResult.Succeeded)
                    {
                        var registerResponse = new RegisterResponse()
                        {
                            Email = registerRequest.Email,
                            Roles = registerRequest.Roles
                        };
                        return Ok(registerResponse);
                    }
                }
                //Add Role
            }
                return BadRequest("Someingth went wrong..");
        }
 

        //Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginrequest)
        {
            var user = await usermanager.FindByEmailAsync(loginrequest.Email);
            if (user != null)
            {
               var validPassword = await usermanager.CheckPasswordAsync(user, loginrequest.Password);
                if (validPassword)
                {
                    //Get Roles
                    var roles = await usermanager.GetRolesAsync(user);

                    if (roles != null)
                    {
                    //createtoken
                    var jwttoken= tokenRepository.CreateJWTToken(user,roles.ToList());

                        var loginresponse = new LoginResponse()
                        {
                            Email = loginrequest.Email,
                            Token = jwttoken.ToString(),
                        };
                            return Ok(loginresponse);
                    }
                }
            }
            return BadRequest("Email and Password incorrect");
        }
    }
}
