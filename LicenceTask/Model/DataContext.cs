using LicenceTask.model;
using Microsoft.EntityFrameworkCore;

namespace LicenceTask.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<UserLicense> UserLicences;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLicense>().ToTable("UserLicense");
        }
    }
}
