
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CardsManagement.Infrastructure.Data
{
    public partial class CoreContext : IdentityDbContext<AppUser, AppRole, Guid
       , IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>,
       IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {

        public CoreContext(DbContextOptions<CoreContext> options
            , IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        #region Tables
        public DbSet<AppUser> AppUsers { get; set; }


        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(
                 Configuration.GetConnectionString(SharedDefinedValues.DefaultConnection));

            }


        }

    }
}
