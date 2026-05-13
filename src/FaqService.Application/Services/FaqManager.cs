using FaqService.Application.DTOs;
using FaqService.Domain.Entities;
using FaqService.Domain.Interfaces;

namespace FaqService.Application.Services;

public class FaqManager(IFaqRepository repository)
{
    public async Task<List<FaqResult>> GetAllAsync()
    {
        var faqs = await repository.GetAllAsync();
        return faqs.Select(ToDto).ToList();
    }

    public async Task<FaqResult?> GetByIdAsync(int id)
    {
        var faq = await repository.GetByIdAsync(id);
        return faq is null ? null : ToDto(faq);
    }

    public async Task<FaqResult?> CreateAsync(FaqRequest request)
    {
        var maxOrder = await repository.GetMaxDisplayOrderAsync();
        var faq = new Faq(request.Title, request.Summary, request.Content, maxOrder + 1);
        var created = await repository.CreateAsync(faq);
        return created is null ? null : ToDto(created);
    }

    public async Task<FaqResult?> UpdateAsync(int id, FaqRequest request)
    {
        var existing = await repository.GetByIdAsync(id);
        if (existing is null) return null;
        existing.UpdateDetails(
            request.Title, request.Summary, request.Content, existing.DisplayOrder);
        
        await repository.UpdateAsync(id, existing);
        return ToDto(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }

    static FaqResult ToDto(Faq faq) 
    => new FaqResult(faq.Id, faq.Title, faq.Summary, faq.Content, faq.DisplayOrder);
}
