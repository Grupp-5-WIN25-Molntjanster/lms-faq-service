namespace FaqService.Domain.Entities;

public class Faq
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int DisplayOrder { get; set; }


    private Faq() { }


    public Faq(string title, string summary, string content, int displayOrder)
    {
        SetTitle(title);
        SetSummary(summary);
        SetContent(content);
        SetDisplayOrder(displayOrder);
    }

    public void UpdateDetails(string title, string summary, string content, int displayOrder)
    {
        SetTitle(title);
        SetSummary(summary);
        SetContent(content);
        SetDisplayOrder(displayOrder);
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (title.Length > 50)
            throw new ArgumentException("Title cannot exceed 50 characters.", nameof(title));
        Title = title;
    }

    private void SetSummary(string summary)
    {
        if (string.IsNullOrWhiteSpace(summary))
            throw new ArgumentException("Summary cannot be empty.", nameof(summary));
        if (summary.Length > 500)
            throw new ArgumentException("Summary cannot exceed 500 characters.", nameof(summary));
        Summary = summary;
    }

    private void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty.", nameof(content));
        if (content.Length > 5000)
            throw new ArgumentException("Content cannot exceed 5000 characters.", nameof(content));
        Content = content;
    }

    private void SetDisplayOrder(int displayOrder)
    {
        if (displayOrder < 0)
            throw new ArgumentException("Display order cannot be negative.", nameof(displayOrder));
        DisplayOrder = displayOrder;
    }
}