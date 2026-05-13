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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var faqs = await faqManager.GetAllAsync();
        return Ok(faqs);
    }

    [HttpGet("{id}")]
    [EndpointName("GetById")]
    [EndpointSummary("Get Faq Article By Id")]
    [EndpointDescription("Returns a single FAQ article by its Id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var faq = await faqManager.GetByIdAsync(id);
        if (faq is null) return NotFound();
        return Ok(faq);
    }

    [HttpPost]
    [EndpointName("CreateArticle")]
    [EndpointSummary("Create a new Faq Article")]
    [EndpointDescription("Creates a new FAQ article and returns the created article")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(FaqRequest request)
    {
        var faq = await faqManager.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = faq!.Id }, faq);
    }

    [HttpPut("{id}")]
    [EndpointName("UpdateArticle")]
    [EndpointSummary("Update an existing Faq Article")]
    [EndpointDescription("Updates an existing FAQ article and returns the updated article")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, FaqRequest request)
    {
        var faq = await faqManager.UpdateAsync(id, request);
        if (faq is null) return NotFound();
        return Ok(faq);
    }

    [HttpDelete("{id}")]
    [EndpointName("DeleteArticle")]
    [EndpointSummary("Delete an existing Faq Article")]
    [EndpointDescription("Deletes an existing FAQ article")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await faqManager.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

}
