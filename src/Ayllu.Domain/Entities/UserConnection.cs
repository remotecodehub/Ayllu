using System.Data;

namespace Ayllu.Domain.Entities;

public class UserConnection
{
    public string User1Id { get; set; } = string.Empty;
    public AppUser User1 { get; set; } = null!;

    public string User2Id { get; set; } = string.Empty;
    public AppUser User2 { get; set; } = null!;

    public ConnectionStatus Status { get; set; } = ConnectionStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}