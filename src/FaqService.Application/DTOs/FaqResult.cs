namespace FaqService.Application.DTOs;

public record FaqResult
    (
    int Id,
    string Title,
    string Summary,
    string Content,
    int DisplayOrder
    );
