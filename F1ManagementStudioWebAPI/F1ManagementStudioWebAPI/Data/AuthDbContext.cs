using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace F1ManagementStudioWebAPI.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "8c77056f-fc14-411e-b00d-a273493d3491";
            var writerRoleId = "8dcaee7b-7968-433f-b53d-b196b480a497";

            var Roles = new List<IdentityRole>
            {
                new IdentityRole
            {
                Id= readerRoleId,
                ConcurrencyStamp=readerRoleId,
                Name="Reader",
                NormalizedName="Reader".ToUpper()
            },
                new IdentityRole
            {
                Id= writerRoleId,
                ConcurrencyStamp=writerRoleId,
                Name="Writer",
                NormalizedName="Writer".ToUpper()
            }
          };
            builder.Entity<IdentityRole>().HasData(Roles);
        }
    }
}
