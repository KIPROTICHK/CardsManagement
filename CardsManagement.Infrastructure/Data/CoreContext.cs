
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static CardsManagement.Application.SharedDefinedValues;

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
        public DbSet<Card> Cards { get; set; }


        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(
                 Configuration.GetConnectionString(SharedDefinedValues.DefaultConnection));

            }


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables
            #region AppRole
            modelBuilder.Entity<AppRole>(entity =>
            {

                entity.Property(x => x.Description)
                .HasMaxLength(50);

                // Each Role can have many entries in the UserRole join table
                entity.HasMany(e => e.UserRoles)
                    .WithOne(e => e.RoleNavigation)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();


            });
            #endregion

            #region AppUser
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(x => x.UserType)
                .HasMaxLength(100)
                .HasDefaultValue(UserType.Member);

                entity.Property(e => e.OTP).IsRequired(false)
                .HasMaxLength(7);

                entity.Property(x => x.FirstName)
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(x => x.LastName).IsRequired(false)
               .HasMaxLength(255);

                entity.Property(x => x.DateCreated)
                .ValueGeneratedOnAdd();

                entity.Property(x => x.ApprovedBy)
                .IsRequired(false)
                 .HasMaxLength(100);

                // Each User can have many entries in the UserRole join table
                entity.HasMany(e => e.UserRoles)
                    .WithOne(e => e.UserNavigation)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            });
            #endregion

            #region Card
            modelBuilder.Entity<Card>(entity =>
            {

                entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);
                 
                entity.HasOne(e => e.OwnerNavigation)
                    .WithMany()
                    .HasForeignKey(ur => ur.Owner)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.Property(x => x.Status)
                .HasMaxLength(100)
                .IsRequired()
                .HasDefaultValue(SharedDefinedValues.CardStatuses.ToDo);

            });
            #endregion

            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
