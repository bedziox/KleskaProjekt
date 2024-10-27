using KleskaProject.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace KleskaProject.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserDetails> UserDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(u => u.Id);

                u.Property(u => u.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();

                u.Property(u => u.LastName)
                    .HasMaxLength(100)
                    .IsRequired();

                u.Property(u => u.Email)
                    .HasMaxLength(255)
                    .IsRequired();

                u.Property(u => u.PasswordHash)
                    .IsRequired();

                u.HasOne(u => u.UserDetails)
                      .WithOne()
                      .HasForeignKey<UserDetails>(ud => ud.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserDetails>(userDetails =>
            {
                userDetails.HasKey(d => d.UserId);

                userDetails.Property(d => d.CreationTime)
                      .IsRequired();

                userDetails.Property(d => d.Address)
                .IsRequired();

                userDetails.Property(d => d.phoneNumber)
                .IsRequired();

                userDetails.Property(d => d.IsActive);
            });


        }
    }
}