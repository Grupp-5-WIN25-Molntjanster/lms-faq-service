namespace FaqService.Application.DTOs;

public record CreateFaqRequest(string Title, string Summary, string Content);