using Microsoft.EntityFrameworkCore;

namespace Crud_Api.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(u => u.IdUser);

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.password)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.HasIndex(u => u.password)
                      .IsUnique();
            });
        }
    }
}
