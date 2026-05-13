namespace FaqService.Application.DTOs;

public record FaqDto
    (
    int Id,
    string Title,
    string Summary,
    string Content,
    int DisplayOrder
    );
