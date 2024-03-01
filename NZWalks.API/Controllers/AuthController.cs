using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NZWalks.WebAPI.CustomActionFilters;
using NZWalks.WebAPI.Models.DTO;
using NZWalks.WebAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace NZWalks.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepositery _token;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepositery token)
        {
            _userManager = userManager;
            _token = token;
        }

        [HttpPost]
        [Route("Register")]
        [ValidateModel]
        public async Task<IActionResult> Register(RegisterRequestDto registerDto)
        {
            var user = new IdentityUser
            {
                UserName = registerDto.UserName.Split("@")[0],
                Email = registerDto.UserName
            };
            
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            if (registerDto.Roles!=null && registerDto.Roles.Any())
            {
               await _userManager.AddToRolesAsync(user, registerDto.Roles);
            }
            return Ok("User was registered! Please ligin");
            
        }
        [HttpPost]
        [Route("Login")]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
           var user =  await _userManager.FindByEmailAsync(loginDto.UserName);  
            
            if(user is null)
            {
                return BadRequest("User Name or password incorrect");
            }
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
            {
                return BadRequest("User Name or password incorrect");
            }

            return Ok
            (
                new JwtToken 
                    { Token = new JwtSecurityTokenHandler().WriteToken(await _token.CreateJWTToken(user))}
            );
        
        }
    }
}
