using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BugTrackingSystem.Models;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Bug> Bugs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bug>()
            .HasOne(b => b.Reporter)
            .WithMany()
            .HasForeignKey(b => b.ReporterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Bug>()
            .HasOne(b => b.Resolver)
            .WithMany()
            .HasForeignKey(b => b.ResolverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
