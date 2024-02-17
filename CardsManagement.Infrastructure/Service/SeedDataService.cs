using CardsManagement.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using static CardsManagement.Application.SharedDefinedValues;

namespace CardsManagement.Infrastructure.Service
{

    public class SeedDataService : ISeedDataService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ISeedAccountSettings _seedAccountSettings;
        private readonly ILogger<SeedDataService> _logger;
 
        public SeedDataService(UserManager<AppUser> userManager
            , RoleManager<AppRole> roleManager
            , ISeedAccountSettings seedAccountSettings
            , ILogger<SeedDataService> logger )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _seedAccountSettings = seedAccountSettings;
            _logger = logger;
         }
         
        public async Task SeedAdmin()
        {
            _logger.LogInformation("\nStart User for seeding admin...");

            var user = new AppUser
            {

                Email = _seedAccountSettings.AdminAccount.Email,
                UserName = _seedAccountSettings.AdminAccount.Email,
                EmailConfirmed = true,
                PhoneNumber = _seedAccountSettings.AdminAccount.PhoneNumber,
                PhoneNumberConfirmed = true,
                UserType = UserType.Admin,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FirstName = _seedAccountSettings.AdminAccount.FirstName,
                LastName = _seedAccountSettings.AdminAccount.LastName,
                Approved = true,
                ApprovedBy = "System",
                DateApproved = DateTime.Now,
                DateCreated = DateTime.Now

            };

            if (!_userManager.Users.Any(u => u.UserName == user.UserName))
            {
                _logger.LogInformation("\nAdmin Data not found, executing seeding func ...\n");
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, _seedAccountSettings.AdminAccount.Password);
                user.PasswordHash = hashed;

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, new string[]
                     { IdentityData.GetCustomRoles()
                    .Where(x => x.Name == IdentityData.Roles.Admin).Select(x => x.Name).FirstOrDefault() });

                    _logger.LogInformation($"Admin seeded successfully with account: {user.Email}\n");
                }
                else
                {
                    _logger.LogError($"Unable to seed admin. \nError: {result.Errors.FirstOrDefault().Description}\n");
                }
            }

            else
            {
                _logger.LogInformation($"\nAdmin Data found with Account: {user.Email} \n");
            }
        }


        public async Task SeedUser()
        {
            _logger.LogInformation("\nStart User for seeding user...");

            var user = new AppUser
            {

                Email = _seedAccountSettings.UserAccount.Email,
                UserName = _seedAccountSettings.UserAccount.Email,
                EmailConfirmed = true,
                PhoneNumber = _seedAccountSettings.UserAccount.PhoneNumber,
                PhoneNumberConfirmed = true,
                UserType = UserType.User,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FirstName = _seedAccountSettings.UserAccount.FirstName,
                LastName = _seedAccountSettings.UserAccount.LastName,
                Approved = true,
                ApprovedBy = "System",
                DateApproved = DateTime.Now,
                DateCreated = DateTime.Now

            };

            if (!_userManager.Users.Any(u => u.UserName == user.UserName))
            {
                _logger.LogInformation("\nAdmin Data not found, executing seeding func ...\n");
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, _seedAccountSettings.UserAccount.Password);
                user.PasswordHash = hashed;

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, new string[]
                     { IdentityData.GetCustomRoles()
                    .Where(x => x.Name == IdentityData.Roles.User).Select(x => x.Name).FirstOrDefault() });

                    _logger.LogInformation($"user seeded successfully with account: {user.Email}\n");
                }
                else
                {
                    _logger.LogError($"Unable to seed user. \nError: {result.Errors.FirstOrDefault().Description}\n");
                }
            }

            else
            {
                _logger.LogInformation($"User Data found with Account: {user.Email} \n");
            }
        }

        public async Task SeedRoles()
        {
            _logger.LogInformation("\nStart roles for seeding...");
            foreach (var role in IdentityData.GetCustomRoles())
            {
                _logger.LogInformation($"\nRole: {role.Name} on proceess");

                if (!_roleManager.Roles.Any(r => r.Name == role.Name))
                {
                    await _roleManager.CreateAsync(new AppRole
                    {
                        Name = role.Name,
                        Description = role.Description,
                        ApplicableToUser = role.ApplicableToUser,
                        ForAdminUser = role.ApplicableToAdmin
                    });

                    _logger.LogInformation($"Role: {role.Name} seeded successfully\n");
                }
                else
                {
                    _logger.LogWarning($"Role: {role.Name} Exists\n");
                }
            }
        }
    }
}
