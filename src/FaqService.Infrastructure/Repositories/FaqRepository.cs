using FaqService.Domain.Entities;
using FaqService.Domain.Interfaces;
using FaqService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FaqService.Infrastructure.Repositories;

public class FaqRepository(FaqDbContext context) : IFaqRepository
{

    public async Task<IEnumerable<Faq>> GetAllAsync()
    {
        return await context.Faqs.OrderBy(f => f.DisplayOrder).ToListAsync();
    }

    public Task<Faq?> GetByIdAsync(int id)
    {
        return context.Faqs.FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Faq?> CreateAsync(Faq faq)
    {
        context.Faqs.Add(faq);
        await context.SaveChangesAsync();
        return faq;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var faq = await context.Faqs.FindAsync(id);
        if (faq is null) return false;

        context.Faqs.Remove(faq);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Faq?> UpdateAsync(int id, Faq faq)
    {
        var exsisting = await context.Faqs.FindAsync(id);
        if(exsisting is null) return null;

        context.Entry(exsisting).CurrentValues.SetValues(faq);
        await context.SaveChangesAsync();
        return exsisting;
    }

    public Task<int> GetMaxDisplayOrderAsync()
    {
        throw new NotImplementedException();
    }
}
