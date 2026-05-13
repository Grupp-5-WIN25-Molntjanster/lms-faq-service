namespace FaqService.Application.DTOs;

public record FaqRequest
    (
    string Title,
    string Summary,
    string Content
    );