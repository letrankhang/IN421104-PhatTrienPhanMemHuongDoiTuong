namespace EduHub.Domain.Entities;

public class Member
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string StudentId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Role { get; private set; } = "Member";
    public DateTime JoinedAt { get; private set; } = DateTime.UtcNow;

    private Member() { }

    public static Member Create(string studentId, string name, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        return new Member { StudentId = studentId, Name = name, Email = email };
    }
}
