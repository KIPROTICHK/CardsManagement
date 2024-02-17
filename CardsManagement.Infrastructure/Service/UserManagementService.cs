using CardsManagement.Application.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static CardsManagement.Application.SharedDefinedValues;

namespace CardsManagement.Infrastructure.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager; 
        public IConfiguration Configuration { get; private set; }

        public UserManagementService(SignInManager<AppUser> signInManager
            ,UserManager<AppUser> userManager 
            , IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
            Configuration = configuration;
        }

        #region token generations
        private string GenerateToken(IEnumerable<Claim> claims, int Hour = Token.Validity.Hours)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["CtSettings:TokenKey"]));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = new DateTimeOffset(DateTime.Now.AddHours(Hour)).DateTime,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims),
                Issuer = "localhost",
                Audience = "localhost"
            };

            JwtSecurityToken stoken = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);

            string token = tokenHandler.WriteToken(stoken);
            return token;

        }

        private static IEnumerable<Claim> GetClaims(AppUser user)
        {
            //var roles=await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            if (user.UserRoles != null)
                foreach (var r in user.UserRoles)
                    claims.Add(new Claim(ClaimTypes.Role, r.RoleNavigation.Name));
            return claims.ToList();
        }

        #endregion
        public async Task<GeneralResponseModel<UserLoginResponse>> Login(LoginRequest request)
        {
            GeneralResponseModel<UserLoginResponse> res = new();
            var data = await _userManager.Users 
              .Where(x => x.UserName == request.Email) 
              .Include(x=>x.UserRoles)
              .FirstOrDefaultAsync();


            if (data == null)
            {
                res.Message = "Account information does not exists";
                return res;
            }
 
            if (!data.EmailConfirmed)
            {
                res.Message = "Please check activation details sent to your email during registration";
                return res;
            }


            var result = await _signInManager.PasswordSignInAsync(data.Email, request.Password, false, lockoutOnFailure: true);
            int TokenHourlyDuration= SharedDefinedValues.TokenHourlyDuration;
             if (result.Succeeded)
            {  
                var token = GenerateToken(GetClaims(data),  TokenHourlyDuration );
                UserLoginResponse userLoginResponse = new()
                {
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName, 
                    ExpiresIn = TokenHourlyDuration* 3600,
                    Token = token,
                    UserType = data.UserType,
                    Id = data.Id,
                };

                res.Data = userLoginResponse;

                res.Success = true;

                return res;

            }
            else if (result.IsLockedOut)
            {
                res.Message = "Account Locked";

                return res;
            }
            else
            {

                res.Message = "Invalid login attempt.";

                return res;
            }
        }
    }
}
