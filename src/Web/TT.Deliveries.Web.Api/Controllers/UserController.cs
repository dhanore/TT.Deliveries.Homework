namespace TT.Deliveries.Web.Api.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TT.Deliveries.Application.Features.UserFeatures;
    using Microsoft.Extensions.Options;
    using TT.Deliveries.Domain.Common;
    using Azure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Azure.Amqp.Framing;
    using Microsoft.Win32;
    using System.Numerics;
    using TT.Deliveries.Data.Dto;
    using Microsoft.Extensions.Configuration;
    using System.Data;

    public class ApplicationUser : IdentityUser
    {

    }


    [Route("users")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        IUserServices userServices;
        //RoleManager<IdentityRole> roleManager;
        JWTSettings jwtSettings;
        public UserController(IUserServices userServices, IOptions<JWTSettings> jwtSettings)//, RoleManager<IdentityRole> roleManager
        {
            this.userServices = userServices;
            this.jwtSettings = jwtSettings.Value;
           // this.roleManager = roleManager;
        }

        //public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        //{
        //    this.userManager = userManager;
        //    this.roleManager = roleManager;
        //    _configuration = configuration;
        //}

        [HttpGet]
        public async Task<JsonResult> Index()
        {
            var res = await userServices.getAllUser();
            return new JsonResult(JsonConvert.SerializeObject(res));
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetOne(string id)
        {
            var res = await userServices.getUserById(id);
            return new JsonResult(JsonConvert.SerializeObject(res));
        }

        [HttpPost]
        public async Task<JsonResult> CreateUser([FromBody] CreateUserRequest user, CancellationToken ct)
        {
            var result = await userServices.createUser(user, ct);

            //if (!await roleManager.RoleExistsAsync(UserRole.Partner.ToString()))
            //    await roleManager.CreateAsync(new IdentityRole(UserRole.Partner.ToString()));
            //if (!await roleManager.RoleExistsAsync(UserRole.User.ToString()))
            //    await roleManager.CreateAsync(new IdentityRole(UserRole.User.ToString()));

            return new JsonResult(new
            {
                statusCode = (int)HttpStatusCode.OK,
                message = JsonConvert.SerializeObject(result)
            });
        }

        [HttpPost("{id}")]
        public async Task<JsonResult> UpdateUser(string id, [FromBody] UpdateUserRequest user, CancellationToken ct)
        {
            await userServices.updateUser(id, user, ct);

            return new JsonResult(new
            {
                statusCode = (int)HttpStatusCode.OK,
                message = "user updated"
            });
        }


        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteUser(string id, CancellationToken ct)
        {
            await userServices.deleteUser(id, ct);

            return new JsonResult(new
            {
                statusCode = (int)HttpStatusCode.OK,
                message = "user deleted"
            });
        }

        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        //{
        //    var userExists = await userManager.FindByNameAsync(model.Username);
        //    if (userExists != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = “Error”, Message = “User already exists!” });

        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Username
        //    };

        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = “Error”, Message = “User creation failed! Please check user details and try again.” });

        //    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //    if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await userManager.AddToRoleAsync(user, UserRoles.Admin);
        //    }

        //    return Ok(new Response { Status = “Success”, Message = “User created successfully!” });
        //}

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest model, CancellationToken ct)
        {
            var user = await userServices.validateUser(model.Email, model.Password, ct);

            if (user != null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

               // authClaims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
                var token = new JwtSecurityToken(
                issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
