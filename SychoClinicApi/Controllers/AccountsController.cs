using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SychoClinicApi.Dtos;
using SychoClinicApi.Helper;
using SychoClinicApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SychoClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Jwt _jwt;
        public AccountsController(UserManager<ApplicationUser> userManager, IOptions<Jwt> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        //registration
        [HttpPost("register")] //https://localhost:44346/api/accounts/register
        public async Task<IActionResult> RegistrationAsync([FromForm] RegisterUserDto dto)
        {
            var ceckEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (ceckEmail != null)
                return BadRequest("Email is aready registered!");

            if (ModelState.IsValid)
            {
                using var dataStream = new MemoryStream();
                await dto.Picture.CopyToAsync(dataStream);
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    UserName = dto.FirstName + dto.LastName,
                    Picture = dataStream.ToArray()
                };
                IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return Ok(new
                    {
                        user.FirstName,
                        user.LastName,
                        user.UserName,
                        user.Email,
                    });
                }

                var erorrs = string.Empty;
                foreach (var erorr in result.Errors)
                    erorrs += $"{erorr.Description},";

                return BadRequest(erorrs);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto dto)
        {
            if (ModelState.IsValid)
            {
                //check
                ApplicationUser user = await _userManager.FindByEmailAsync(dto.Email);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, dto.Password);
                    if (found)
                    {

                        var claimsList = new List<Claim>();
                        claimsList.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claimsList.Add(new Claim(ClaimTypes.Email, user.Email));
                        claimsList.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claimsList.Add(new Claim(ClaimTypes.Role, role));
                        }

                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
                        SigningCredentials singincrad = new SigningCredentials(
                            securityKey,
                            SecurityAlgorithms.HmacSha256
                            );
                        //Create Token
                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: _jwt.Issuer,
                            audience: _jwt.Audience,
                            claims: claimsList,
                            expires: DateTime.Now.AddDays(_jwt.Duration),
                             signingCredentials: singincrad
                            );
                        return Ok(new
                        {
                            user.FirstName,
                            user.LastName,
                            user.UserName,
                            user.Email,
                            user.Picture,
                            role = roles.FirstOrDefault(),
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                        });
                    }
                    return Unauthorized("Password is incorrect!");
                }
                return Unauthorized("Email is incorrect!");
            }
            return BadRequest(ModelState);
        }
    }
}
