using FaqService.Domain.Entities;
using FaqService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
namespace FaqService.Api.Seed;

public static class DataSeeder
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FaqDbContext>();
        await context.Database.MigrateAsync();
        if (!await context.Faqs.AnyAsync())
        {
            context.Faqs.AddRange(
                new Faq("What is the return policy?", "You can return items within 30 days for a full refund.", "Full details...", 1),
                new Faq("How do I track my order?", "Once shipped, you will receive a tracking number via email.", "Full details...", 2),
                new Faq("What payment methods are accepted?", "We accept Visa, Mastercard, PayPal, and Swish.", "Full details...", 3)
            );
            await context.SaveChangesAsync();
        }
    }
}