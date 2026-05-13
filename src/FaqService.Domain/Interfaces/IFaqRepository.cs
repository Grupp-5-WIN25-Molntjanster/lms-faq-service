using FaqService.Domain.Entities;

namespace FaqService.Domain.Interfaces;

public interface IFaqRepository
{
    Task<IEnumerable<Faq>> GetAllAsync();

    Task<Faq?> GetByIdAsync(int id);

    Task<Faq?> CreateAsync(Faq faq);

    Task<Faq?> UpdateAsync(int id, Faq faq);

    Task<bool> DeleteAsync(int id);

    Task<int> GetMaxDisplayOrderAsync();
}
