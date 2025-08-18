namespace Ayllu.Domain.Entities;

public class InvalidatedToken
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Jwt { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public DateTime InvalidatedAt { get; set; } = DateTime.UtcNow;
}