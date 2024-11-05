using KleskaProject.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserDetails> UserDetails { get; set; } = null!;
    public DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User
        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(u => u.Id);
            u.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            u.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            u.Property(u => u.Email).IsRequired().HasMaxLength(255);
            u.Property(u => u.PasswordHash).IsRequired();

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

            u.HasOne(u => u.UserDetails)
              .WithOne()
              .HasForeignKey<UserDetails>(ud => ud.UserId)
              .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure UserDetails
        modelBuilder.Entity<UserDetails>(ud =>
        {
            ud.HasKey(ud => ud.UserId);
            ud.Property(ud => ud.CreationTime).IsRequired();
            ud.Property(ud => ud.IsActive).IsRequired();

            ud.HasOne(ud => ud.phoneNumber)
              .WithOne()
              .HasForeignKey<PhoneNumber>(pn => pn.UserDetailsId)
              .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure PhoneNumber
        modelBuilder.Entity<PhoneNumber>(pn =>
        {
            pn.HasKey(pn => pn.Id);
            pn.Property(pn => pn.CountryCode).IsRequired();
            pn.Property(pn => pn.Number).IsRequired();
        });
    }
}