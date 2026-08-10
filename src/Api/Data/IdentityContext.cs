using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class IdentityContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options) {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("users");
            builder.Entity<IdentityRole>().ToTable("roles");

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("user_claims");

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("user_logins");

            builder.Entity<IdentityUserToken<string>>()
                .ToTable("user_tokens");

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("role_claims");

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("user_roles");
        }
    };


}
