using AroundTheWorld.ViewModels.IdentityModels;
using AroundTheWorld_Backend.Interfaces;
using AroundTheWorld_Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AroundTheWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserPositionService _userPositionService;

        public IdentityController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IUserPositionService userPositionService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userPositionService = userPositionService;
        }

        [HttpGet]
        [Route("GetIdByEmail")]
        public async Task<IActionResult> GetIdByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user.Id);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(8),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    userId = user.Id,
                    userRole = userRoles[0],
                    userName = user.UserName,
                    companyId = user.CompanyId
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register-traveler")]
        public async Task<IActionResult> Register([FromBody] TravelerRegistrationModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                CompanyId = "user"
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var registeredUser = await _userManager.FindByEmailAsync(model.Email);
            _userPositionService.AddUserPosition(registeredUser.Id);
            return Ok(new
            {
                userId = user.Id,
                userRole = userRoles[0]
            });
        }

        [HttpPost]
        [Route("register-worker")]
        public async Task<IActionResult> RegisterWorker([FromBody] GuideRegistrationViewModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                CompanyId = model.CompanyId
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            if (!await _roleManager.RoleExistsAsync("Worker"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Worker"));
            }

            if (await _roleManager.RoleExistsAsync("Worker"))
            {
                await _userManager.AddToRoleAsync(user, "Worker");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var registeredUser = await _userManager.FindByEmailAsync(model.Email);
            _userPositionService.AddUserPosition(registeredUser.Id);
            return Ok(new
            {
                userId = user.Id,
                userRole = userRoles[0]
            });
        }

        [HttpPost]
        [Route("register-guide")]
        public async Task<IActionResult> RegisterGuide([FromBody] GuideRegistrationViewModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                CompanyId = model.CompanyId
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            if (!await _roleManager.RoleExistsAsync("Guide"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Guide"));
            }

            if (await _roleManager.RoleExistsAsync("Guide"))
            {
                await _userManager.AddToRoleAsync(user, "Guide");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var registeredUser = await _userManager.FindByEmailAsync(model.Email);
            _userPositionService.AddUserPosition(registeredUser.Id);
            return Ok(new
            {
                userId = user.Id,
                userRole = userRoles[0]
            });
        }
    }
}
