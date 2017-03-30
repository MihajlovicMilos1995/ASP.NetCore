using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace People.API.Models
{
    public class PeopleContext : IdentityDbContext<IdentityUser>
    {
        public PeopleContext(DbContextOptions<PeopleContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet <PeopleModel> People { get; set; }

    }
}
