using System.ComponentModel.DataAnnotations;

namespace FaqService.Application.DTOs;

public record FaqRequest
    (
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
    string Title,

    [Required(ErrorMessage = "Summary is required")]
    [MaxLength(500, ErrorMessage = "Summary cannot exceed 500 characters")]
    string Summary,

    [Required(ErrorMessage = "Content is required")]
    [MaxLength(5000, ErrorMessage = "Content cannot exceed 5000 characters")]
    string Content
    );