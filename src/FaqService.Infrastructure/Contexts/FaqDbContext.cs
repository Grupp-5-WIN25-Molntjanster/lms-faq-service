using FaqService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaqService.Infrastructure.Contexts;

public class FaqDbContext(DbContextOptions<FaqDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Domain.Entities.Faq>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Summary).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(5000);
            entity.Property(e => e.DisplayOrder).IsRequired();
        });
    }

    public DbSet<Faq> Faqs => Set<Faq>();
}
