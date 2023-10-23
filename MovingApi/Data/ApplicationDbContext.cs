using Microsoft.EntityFrameworkCore;
using MovingApi.Models;

namespace MovingApi.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Genre>().Property(g=>g.Id).UseIdentityColumn();
            base.OnModelCreating(builder);

        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
