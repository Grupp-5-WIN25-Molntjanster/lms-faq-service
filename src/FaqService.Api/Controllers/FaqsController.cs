using FaqService.Application.DTOs;
using FaqService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FaqService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class FaqsController(FaqManager faqManager) : ControllerBase
{

    [HttpGet]
    [EndpointName("GetAllFAQs")]
    [EndpointSummary("Get All Faq Articles")]
    [EndpointDescription("Returns a list of all FAQ articles")]
    public async Task<IActionResult> GetAll()
    {
        var faqs = await faqManager.GetAllAsync();
        return Ok(faqs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var faq = await faqManager.GetByIdAsync(id);
        if (faq is null) return NotFound();
        return Ok(faq);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFaqRequest request)
    {
        var faq = await faqManager.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = faq!.Id }, faq);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateFaqRequest request)
    {
        var faq = await faqManager.UpdateAsync(id, request);
        if (faq is null) return NotFound();
        return Ok(faq);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await faqManager.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

}
