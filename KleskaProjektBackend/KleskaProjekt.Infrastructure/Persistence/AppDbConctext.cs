using Microsoft.EntityFrameworkCore;
using KleskaProject.Domain.Entities; 

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Announcement> Announcements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200); 

            entity.Property(e => e.Description)
                  .HasMaxLength(1000); 

            entity.Property(e => e.Username)
                  .IsRequired() 
                  .HasMaxLength(100); 

            entity.Property(e => e.Type)
                  .HasConversion<string>() 
                  .IsRequired(); 

            entity.Property(e => e.ExpirationDate)
                  .IsRequired(); 

            entity.Property(e => e.Likes)
                  .HasDefaultValue(0); 
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Nickname)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Rank)
                  .HasConversion<string>() 
                  .IsRequired();

            entity.Property(e => e.Age)
                  .IsRequired();

            entity.Property(e => e.Location)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.Seeking)
                  .HasConversion<string>() 
                  .IsRequired();

            entity.Property(e => e.ReviewsCount)
                  .HasDefaultValue(0);

            entity.Property(e => e.AverageRating)
                  .HasDefaultValue(0.0);
        });
    }
}